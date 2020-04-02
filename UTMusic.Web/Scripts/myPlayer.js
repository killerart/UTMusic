jQuery(function ($) {
	trackCount = trackCount = document.getElementById('plUserList').children.length + document.getElementById('plList').children.length;
	$(".plItem[file='" + fileName + "']").first().parent().addClass("plSel");
	$('#plList .plItem').each(function (id, element) {
		element.onclick =  function () {
			var userSongCount = document.getElementById('plUserList').children.length;
			if (id + userSongCount !== index) {
				playTrack(id + userSongCount);
			} else if (playing) {
				audio.pause();
			} else {
				audio.play();
			}
		};
	});
	$('#plUserList .plItem').each(function (id, element) {
		element.onclick = function () {
			if (id !== index) {
				playTrack(id);
			} else if (playing) {
				audio.pause();
			} else {
				audio.play();
			}
		};
	});
});