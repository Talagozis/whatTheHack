var TimeDomain = (function(){
	var instance = {};

	var audioContext;
	var sourceNode;
	var analyserNode;
	var javascriptNode;
	var sampleSize = 1024; // samples to collect before analysing
	var amplitudeArray; // holds frequency amplitude data
	var audioStream;
	var lowPassFilter;

	instance.setAudioContext = function(ac){
		audioContext = new ac();
		//console.log("audio context:");
		//console.log(audioContext);
	};

	instance.setAudioStream = function(stream){
		audioStream = stream;
		//console.log("audio stream");
		//console.log(audioStream);
	};

	instance.start = function(){
		sourceNode = audioContext.createMediaStreamSource(audioStream);
		analyserNode = audioContext.createAnalyser();
		javascriptNode = audioContext.createScriptProcessor(sampleSize, 1, 1);
		lowPassFilter = audioContext.createBiquadFilter();
		lowPassFilter.type = "LOWPASS";
		lowPassFilter.frequency.value = 500;
		amplitudeArray = new Uint8Array(analyserNode.frequencyBinCount);
		//triggered when enough samples collected
		javascriptNode.onaudioprocess = function(){
			amplitudeArray = new Uint8Array(analyserNode.frequencyBinCount);
			analyserNode.getByteTimeDomainData(amplitudeArray);
		};

		// Draw the audio graph
		/*
		sourceNode.connect(analyserNode);
		analyserNode.connect(javascriptNode);
		javascriptNode.connect(audioContext.destination);
		*/
		sourceNode.connect(lowPassFilter);
		lowPassFilter.connect(analyserNode);
		analyserNode.connect(javascriptNode);
		javascriptNode.connect(audioContext.destination);
	};

	instance.stop = function(){
		javascriptNode.onaudioprocess = null;
		/*
		if(audioStream){
			if(audioStream.stop){
				audioStream.stop();
			}
		} 
		*/
		if(sourceNode) sourceNode.disconnect();
	};

	instance.getProcessedData = function(){
		//console.log(amplitudeArray);
		return amplitudeArray;
	};

	return instance;
});
