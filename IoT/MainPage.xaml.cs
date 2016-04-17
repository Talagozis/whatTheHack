using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel;
using System.Threading.Tasks;
using test4.ServiceReference;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.Media.Audio;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace test4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private double theta = 0;

        private AudioGraph audioGraph;

        private AudioDeviceInputNode deviceInputNode;
        private AudioDeviceOutputNode deviceOutputNode;

        private AudioFileInputNode fileInputNode;
        private AudioFileOutputNode fileOutputNode;

        private AudioFrameInputNode frameInputNode;
        private AudioFrameOutputNode frameOutputNode;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //BasicHttpBinding binding = new BasicHttpBinding();
            //EndpointAddress address = new EndpointAddress("http://192.168.0.109:9000/wcf/snoring.svc");
            
            test1();
        }

        private async void test1()
        {
            AudioGraphSettings audioGraphSettings = new AudioGraphSettings(Windows.Media.Render.AudioRenderCategory.Media);
            audioGraphSettings.DesiredSamplesPerQuantum = 10;
            audioGraphSettings.QuantumSizeSelectionMode = QuantumSizeSelectionMode.LowestLatency;
            audioGraphSettings.DesiredRenderDeviceAudioProcessing = Windows.Media.AudioProcessing.Raw;

            CreateAudioGraphResult createAudioGraphResult = await AudioGraph.CreateAsync(audioGraphSettings);
            if (createAudioGraphResult.Status != AudioGraphCreationStatus.Success)
            {
                Debug.WriteLine("AudioGraph creation error: " + createAudioGraphResult.Status.ToString());
                return;
            }

            audioGraph = createAudioGraphResult.Graph;



            CreateAudioDeviceInputNodeResult createAudioDeviceInputNodeResult = await audioGraph.CreateDeviceInputNodeAsync(MediaCategory.Media);

            if (createAudioDeviceInputNodeResult.Status != AudioDeviceNodeCreationStatus.Success)
            {
                Debug.WriteLine(createAudioDeviceInputNodeResult.Status.ToString());
                return;
            }

            deviceInputNode = createAudioDeviceInputNodeResult.DeviceInputNode;



            CreateAudioDeviceOutputNodeResult createAudioDeviceOutputNodeResult = await audioGraph.CreateDeviceOutputNodeAsync();

            if (createAudioDeviceOutputNodeResult.Status != AudioDeviceNodeCreationStatus.Success)
            {
                Debug.WriteLine(createAudioDeviceOutputNodeResult.Status.ToString());
                return;
            }

            deviceOutputNode = createAudioDeviceOutputNodeResult.DeviceOutputNode;


            await CreateFileOutputNode();


            AudioSubmixNode submixNode = audioGraph.CreateSubmixNode();

            deviceInputNode.AddOutgoingConnection(submixNode);
            submixNode.AddOutgoingConnection(deviceOutputNode);
            submixNode.AddOutgoingConnection(fileOutputNode);

            EqualizerEffectDefinition equalizerEffectDefinition = new EqualizerEffectDefinition(audioGraph);


            equalizerEffectDefinition.Bands[0].FrequencyCenter = 50.0f;
            equalizerEffectDefinition.Bands[0].Gain = 0.0f;
            equalizerEffectDefinition.Bands[0].Bandwidth = 1.0f;

            equalizerEffectDefinition.Bands[2].FrequencyCenter = 200.0f;
            equalizerEffectDefinition.Bands[2].Gain = 5.0f;
            equalizerEffectDefinition.Bands[2].Bandwidth = 1.0f;

            equalizerEffectDefinition.Bands[1].FrequencyCenter = 1700.0f;
            equalizerEffectDefinition.Bands[1].Gain = 0.0f;
            equalizerEffectDefinition.Bands[1].Bandwidth = 1.7f;

            submixNode.EffectDefinitions.Add(equalizerEffectDefinition);

            
            audioGraph.Start();
            deviceInputNode.Start();
            deviceOutputNode.Start();
            fileOutputNode.Start();

            Task.Delay(1000).Wait();

            deviceInputNode.Stop();
            deviceOutputNode.Stop();
            fileOutputNode.Stop();
            await fileOutputNode.FinalizeAsync();
            audioGraph.Stop();


            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.GetFileAsync("sample.wav");
            byte[] bytes = (await Windows.Storage.FileIO.ReadBufferAsync(file)).ToArray();
            SnoringClient snoring = new SnoringClient();
            Debug.WriteLine(await snoring.uploadAudioAsync(bytes));
        }


        private async Task CreateFileInputNode()
        {
            if (audioGraph == null)
                return;

            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.SuggestedStartLocation = PickerLocationId.MusicLibrary;
            filePicker.FileTypeFilter.Add(".mp3");
            filePicker.FileTypeFilter.Add(".wav");
            filePicker.FileTypeFilter.Add(".wma");
            filePicker.FileTypeFilter.Add(".m4a");
            filePicker.ViewMode = PickerViewMode.Thumbnail;
            StorageFile file = await filePicker.PickSingleFileAsync();

            // File can be null if cancel is hit in the file picker
            if (file == null)
            {
                return;
            }
            CreateAudioFileInputNodeResult result = await audioGraph.CreateFileInputNodeAsync(file);

            if (result.Status != AudioFileNodeCreationStatus.Success)
            {
                Debug.WriteLine(result.Status.ToString());
            }

            fileInputNode = result.FileInputNode;
        }

        private async Task CreateFileOutputNode()
        {
            FileSavePicker saveFilePicker = new FileSavePicker();
            saveFilePicker.FileTypeChoices.Add("Pulse Code Modulation", new List<string>() { ".wav" });
            saveFilePicker.FileTypeChoices.Add("Windows Media Audio", new List<string>() { ".wma" });
            saveFilePicker.FileTypeChoices.Add("MPEG Audio Layer-3", new List<string>() { ".mp3" });
            saveFilePicker.SuggestedFileName = "New Audio Track";

            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.CreateFileAsync("sample.wav", CreationCollisionOption.ReplaceExisting);

            //StorageFile file = await storageFolder.GetFileAsync("sample.wav");

            // File can be null if cancel is hit in the file picker
            if (file == null)
            {
                return;
            }

            MediaEncodingProfile mediaEncodingProfile;
            switch (file.FileType.ToString().ToLowerInvariant())
            {
                case ".wma":
                    mediaEncodingProfile = MediaEncodingProfile.CreateWma(AudioEncodingQuality.High);
                    break;
                case ".mp3":
                    mediaEncodingProfile = MediaEncodingProfile.CreateMp3(AudioEncodingQuality.High);
                    break;
                case ".wav":
                    mediaEncodingProfile = MediaEncodingProfile.CreateWav(AudioEncodingQuality.High);
                    break;
                default:
                    throw new ArgumentException();
            }


            // Operate node at the graph format, but save file at the specified format
            CreateAudioFileOutputNodeResult result = await audioGraph.CreateFileOutputNodeAsync(file, mediaEncodingProfile);

            if (result.Status != AudioFileNodeCreationStatus.Success)
            {
                // FileOutputNode creation failed
                Debug.WriteLine(result.Status.ToString());
                return;
            }

            fileOutputNode = result.FileOutputNode;
        }

        private void CreateFrameInputNode()
        {
            // Create the FrameInputNode at the same format as the graph, except explicitly set mono.
            AudioEncodingProperties nodeEncodingProperties = audioGraph.EncodingProperties;
            nodeEncodingProperties.ChannelCount = 1;
            frameInputNode = audioGraph.CreateFrameInputNode(nodeEncodingProperties);

            // Initialize the Frame Input Node in the stopped state
            frameInputNode.Stop();

            // Hook up an event handler so we can start generating samples when needed
            // This event is triggered when the node is required to provide data
            frameInputNode.QuantumStarted += node_QuantumStarted;
        }

        private void node_QuantumStarted(AudioFrameInputNode sender, FrameInputNodeQuantumStartedEventArgs args)
        {
            // GenerateAudioData can provide PCM audio data by directly synthesizing it or reading from a file.
            // Need to know how many samples are required. In this case, the node is running at the same rate as the rest of the graph
            // For minimum latency, only provide the required amount of samples. Extra samples will introduce additional latency.
            uint numSamplesNeeded = (uint)args.RequiredSamples;

            if (numSamplesNeeded != 0)
            {
                AudioFrame audioData = GenerateAudioData(numSamplesNeeded);
                frameInputNode.AddFrame(audioData);
            }
        }

        unsafe private AudioFrame GenerateAudioData(uint samples)
        {
            // Buffer size is (number of samples) * (size of each sample)
            // We choose to generate single channel (mono) audio. For multi-channel, multiply by number of channels
            uint bufferSize = samples * sizeof(float);
            AudioFrame frame = new Windows.Media.AudioFrame(bufferSize);

            using (AudioBuffer buffer = frame.LockBuffer(AudioBufferAccessMode.Write))
            using (IMemoryBufferReference reference = buffer.CreateReference())
            {
                byte* dataInBytes;
                uint capacityInBytes;
                float* dataInFloat;

                // Get the buffer from the AudioFrame
                ((IMemoryBufferByteAccess)reference).GetBuffer(out dataInBytes, out capacityInBytes);

                // Cast to float since the data we are generating is float
                dataInFloat = (float*)dataInBytes;

                float freq = 1000; // choosing to generate frequency of 1kHz
                float amplitude = 0.3f;
                int sampleRate = (int)audioGraph.EncodingProperties.SampleRate;
                double sampleIncrement = (freq * (Math.PI * 2)) / sampleRate;

                // Generate a 1kHz sine wave and populate the values in the memory buffer
                for (int i = 0; i < samples; i++)
                {
                    double sinValue = amplitude * Math.Sin(theta);
                    dataInFloat[i] = (float)sinValue;
                    theta += sampleIncrement;
                }
            }

            return frame;
        }

        private void CreateFrameOutputNode()
        {
            frameOutputNode = audioGraph.CreateFrameOutputNode();
            audioGraph.QuantumProcessed += AudioGraph_QuantumProcessed;
        }

        private void AudioGraph_QuantumProcessed(AudioGraph sender, object args)
        {
            AudioFrame frame = frameOutputNode.GetFrame();
            ProcessFrameOutput(frame);

        }

        unsafe private void ProcessFrameOutput(AudioFrame frame)
        {
            using (AudioBuffer buffer = frame.LockBuffer(AudioBufferAccessMode.Write))
            using (IMemoryBufferReference reference = buffer.CreateReference())
            {
                byte* dataInBytes;
                uint capacityInBytes;
                float* dataInFloat;

                // Get the buffer from the AudioFrame
                ((IMemoryBufferByteAccess)reference).GetBuffer(out dataInBytes, out capacityInBytes);

                dataInFloat = (float*)dataInBytes;
            }
        }




    }
}
