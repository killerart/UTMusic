// Mythium Archive: https://archive.org/details/mythium/

jQuery(function ($) {
	'use strict'
	var supportsAudio = !!document.createElement('audio').canPlayType;
	if (supportsAudio) {
		// initialize plyr
		var player = new Plyr('#audio1', {
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
		});
		// initialize playlist and controls
		var index = 0,
			playing = false,
			mediaPath = '/Music/',
			extension = '.mp3',
			trackCount = document.getElementById('plUserList').children.length + document.getElementById('plList').children.length,
			npAction = $('#npAction'),
			npTitle = $('#npTitle'),
			audio = $('#audio1').on('play', function () {
				playing = true;
				npAction.text('Now Playing...');
			}).on('pause', function () {
				playing = false;
				npAction.text('Paused...');
			}).on('ended', function () {
				npAction.text('Paused...');
				if ((index + 1) < trackCount) {
					index++;
					loadTrack(index);
					audio.play();
				} else {
					audio.pause();
					index = 0;
					loadTrack(index);
				}
			}).get(0);
		function loadTrack(id) {
			$('.plSel').removeClass('plSel');
			var idName = "#plUserList";
			var newId = id;
			var userSongCount = document.getElementById('plUserList').children.length;
			if (id >= userSongCount) {
				idName = "#plList";
				newId = id - userSongCount;
			}
			$(idName + ' li:eq(' + newId + ')').addClass('plSel');
			var songName = $(idName + ' li:eq(' + newId + ') .plTitle').text();
			npTitle.text(songName);
			var fileName = $(idName + ' li:eq(' + newId + ') .plItem').attr("file");
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
		$('#btnPrev').on('click', function () {
			if (index > 0) {
				index--;
				loadTrack(index);
				if (playing) {
					audio.play();
				}
			} else {
				audio.pause();
				/*index = 0;*/
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
		$('#plList li').on('click', function () {
			var id = parseInt($(this).index()) + document.getElementById('plUserList').children.length;
			if (id !== index) {
				playTrack(id);
			} else if (playing) {
				audio.pause();
			} else {
				audio.play();
			}
		})
		$('#plUserList li').on('click', function () {
			var id = parseInt($(this).index());
			if (id !== index) {
				playTrack(id);
			} else if (playing) {
				audio.pause();
			} else {
				audio.play();
			}
		})
		loadTrack(index);
	}
});
