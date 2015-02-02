/**
 * Created by andycall on 15-1-29.
 */

$.fn.serializeObject = function(){
	var o = {};
	var a = this.serializeArray();

	$.each(a, function(){
		if( o[ this.name ] != undefined){
			if(! o[ this.name ].push){
				o[ this.name ] = [o[this.name]];
			}
			o[this.name].push( this.value || "")
		}
		else{
			o[this.name] = this.value || "";
		}
	});
	return o;
};