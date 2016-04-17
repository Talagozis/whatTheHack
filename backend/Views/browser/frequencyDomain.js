var FrequencyDomain = (function(){
	var instance = {};

	var audioContext;
	var audioBuffer;
	var sourceNode;
	var analyserNode;
	var audioData = null;
	var sampleSize = 1024;
	var fftSize = 1024;
	var frequencyArray;
	var javascriptNode;

	instance.setAudioContext = function(ac){
		audioContext = new ac();
	};

	instance.setAudioStream = function(stream){
		audioStream = stream;
		//console.log("audio stream");
		//console.log(audioStream);
	};

	instance.start = function(){
		sourceNode = audioContext.createMediaStreamSource(audioStream);
		//console.log(audioContext);
		analyserNode = audioContext.createAnalyser();
		analyserNode.smoothingTimeConstant = 0.0;
		analyserNode.fftSize = fftSize;
		javascriptNode = audioContext.createScriptProcessor(sampleSize, 1, 1);
		javascriptNode.onaudioprocess = function(){
			frequencyArray = new Uint8Array(analyserNode.frequencyBinCount);
			analyserNode.getByteFrequencyData(frequencyArray);
			//console.log(frequencyArray);
		};

		sourceNode.connect(audioContext.destination);
		sourceNode.connect(analyserNode);
		analyserNode.connect(javascriptNode);
		javascriptNode.connect(audioContext.destination);
	};

	instance.stop = function(){
		javascriptNode.onaudioprocess = null;
		if(audioStream) audioStream.stop();
		if(sourceNode) sourceNode.disconnect();
	};

	instance.getProcessedData = function(){
		//console.log(frequencyArray);
		return frequencyArray;
	};

	return instance;
});
