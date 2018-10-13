export default {
  playlists: [],

  createPlaylist(name) {
    const newPlaylist = {
      name: name,

      musics: [],
      
      addMusic(music) {
        this.musics.push(music);
      }
    }

    this.playlists.push(newPlaylist);

    return newPlaylist
  }
}