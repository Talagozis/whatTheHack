(function e(t,n,r){function s(o,u){if(!n[o]){if(!t[o]){var a=typeof require=="function"&&require;if(!u&&a)return a(o,!0);if(i)return i(o,!0);var f=new Error("Cannot find module '"+o+"'");throw f.code="MODULE_NOT_FOUND",f}var l=n[o]={exports:{}};t[o][0].call(l.exports,function(e){var n=t[o][1][e];return s(n?n:e)},l,l.exports,e,t,n,r)}return n[o].exports}var i=typeof require=="function"&&require;for(var o=0;o<r.length;o++)s(r[o]);return s})({1:[function(require,module,exports){
module.exports = (function(){
	var instance = {};

	var audioContext;
	var sourceNode;
	var analyserNode;
	var javascriptNode;
	var sampleSize = 1024; // samples to collect before analysing
	var amplitudeArray; // holds frequency amplitude data
	var audioStream;

	instance.setAudioContext = function(ac){
		audioContext = ac;
	};

	instance.setAudioStream = function(stream){
		audioStream = stream;
	};

	instance.start = function(){
		sourceNode = audioContext.createMediaStreamSource(audioStream);
		analyserNode = audioContext.createAnalyser();
		javascriptNode = audioContext.createScriptProcessor(sampleSize, 1, 1);
		amplitudeArray = new Uint8Array(analyserNode.frequencyBinCount);
		//triggered when enough samples collected
		javascriptNode.onaudioprocess = function(){
			amplitudeArray = new Uint8Array(analyserNode.frequencyBinCount);
			analyserNode.getByteTimeDomain(amplitudeArray);
		};

		// Draw the audio graph
		sourceNode.connect(analyserNode);
		analyserNode.connect(javascriptNode);
		javascriptNode.connect(audioContext.destination);
	};

	instance.getProcessedData = function(){
		return amplitudeArray;
	};

	return instance;
});

},{}]},{},[1]);
