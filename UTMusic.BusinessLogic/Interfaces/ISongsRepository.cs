using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTMusic.Data.Entities;

namespace UTMusic.BusinessLogic.Interfaces
{
    /// <summary>
    /// Репозиторий песен
    /// </summary>
    public interface ISongsRepository
    {
        /// <summary>
        /// Получить все песни из базы данных
        /// </summary>
        /// <returns>Массив List со всеми песнями</returns>
        ICollection<Song> GetAllSongs();
        /// <summary>
        /// Найти песню по ID
        /// </summary>
        /// <param name="id">ID песни</param>
        /// <returns>Песня или null, если нет песни с таким ID</returns>
        Song GetSongById(int id);
        /// <summary>
        /// Найти песню по названию
        /// </summary>
        /// <param name="id">Название песни</param>
        /// <returns>Песня или null, если нет песни с таким названием</returns>
        Song GetSongByName(string name);
        /// <summary>
        /// Найти песню по имени файла
        /// </summary>
        /// <param name="id">Имя файла песни</param>
        /// <returns>Песня или null, если нет песни с таким именем файла</returns>
        Song GetSongByFileName(string fileName);
        /// <summary>
        /// Сохранить новую песню в базе данных или обновить существующую
        /// </summary>
        /// <param name="song">Песня, которую надо сохранить</param>
        /// <returns>Сохраненная песня</returns>
        Song SaveSong(Song song);
        /// <summary>
        /// Удалить песню из базы данных
        /// </summary>
        /// <param name="song">Песня, которую надо удалить</param>
        void DeleteSong(Song song);
    }
}
