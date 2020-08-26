// Mythium Archive: https://archive.org/details/mythium/

// initialize plyr
var player, index, playing, mediaPath, extension, trackCount, npAction, npTitle, audio, fileName, plUserList, userSongCount, idName;
function loadTrack(id) {
	$('.plSel').removeClass('plSel');
	var newId = id;

	idName = "#plUserList";
	if (id >= userSongCount) {
		idName = "#plList";
		newId = id - userSongCount;
	}
	$(idName + ' > .row:eq(' + newId + ')').addClass('plSel');
	var songName = $(idName + ' > .row:eq(' + newId + ') .plTitle').text();
	npTitle.text(songName);
	fileName = $(idName + ' > .row:eq(' + newId + ') .plItem').attr("file");
	index = id;
	audio.src = mediaPath + fileName + extension;
	updateDownload(id, audio.src);
}
function updateDownload(_id, source) {
	player.on('loadedmetadata', function () {
		$('a[data-plyr="download"]').attr('href', source);
	});
}
function playTrack(id) {
	loadTrack(id);
	audio.play();
}
jQuery(function () {
	player = new Plyr('#audio1', {
		controls: [
			'restart',
			'play',
			'progress',
			'current-time',
			'duration',
			'mute',
			'volume',
			'download'
		]
	}),
		index = 0,
		playing = false,
		mediaPath = '/Music/',
		extension = '.mp3',
		npAction = $('#npAction'),
		npTitle = $('#npTitle'),
		audio = $('#audio1').on('play', function () {
			playing = true;
			npAction.text('Playing');
		}).on('pause', function () {
			playing = false;
			npAction.text('Paused');
		}).on('ended', function () {
			npAction.text('Paused');
			if ((index + 1) < trackCount) {
				index++;
				loadTrack(index);
				audio.play();
			} else {
				audio.pause();
				index = 0;
				loadTrack(index);
			}
		}).get(0),
		plUserList = document.getElementById('plUserList'),
		userSongCount = plUserList != null ? plUserList.children.length : 0,
		song = document.getElementsByClassName("plItem")[0],
		fileName = "";
	if (song)
		fileName = song.getAttribute("file");
	$('#btnPrev').on('click', function () {
		if (index > 0) {
			index--;
			loadTrack(index);
			if (playing) {
				audio.play();
			}
		} else {
			audio.pause();
			index = 0;
			loadTrack(index);
		}
	})
	$('#btnNext').on('click', function () {
		if (index < trackCount - 1) {
			index++;
			loadTrack(index);
			if (playing) {
				audio.play();
			}
		} else {
			audio.pause();
			index = 0;
			loadTrack(index);
		}
	})
	loadTrack(index);
});
