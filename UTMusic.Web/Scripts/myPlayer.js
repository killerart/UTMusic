jQuery(function ($) {
	plUserList = document.getElementById('plUserList');
	userSongCount = plUserList != null ? plUserList.children.length : 0;
	trackCount = trackCount = plUserList != null ? plUserList.children.length : 0 + document.getElementById('plList').children.length;
	$(".plItem[file='" + fileName + "']").first().parent().addClass("plSel");
	$('#plList .plItem').each(function (id, element) {
		element.onclick = function () {
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