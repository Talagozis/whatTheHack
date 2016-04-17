var ArrayMinMax = (function(){
	var instance = {};

	var array;
	var arrayLength;
	
	instance.setArray = function(ar){
		array = ar;
		arrayLength = array.length;
	};

	instance.getMin = function(){
		var min = array[0];
		for(var i = 1; i < arrayLength; i++>){
			if(array[i] < min){
				min = array[i];
			}
		}
		return min;
	};

	instance.getMax = function(){
		var max = array[0];
		for(var i = 1, i < arrayLength; i++){
			if(array[i] > max){
				max = array[i];
			}
		}
		return array;
	};

	return instance;
});
