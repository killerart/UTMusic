jQuery(function ($) {
	plUserList = document.getElementById('plUserList');
	userSongCount = plUserList != null ? plUserList.children.length : 0;
	trackCount = userSongCount + document.getElementById('plList').children.length;
	var songs = $(idName + ' .plItem[file="' + fileName + '"]');
	songs.first().parent().addClass("plSel");
	index = $(".plItem").index(songs);

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