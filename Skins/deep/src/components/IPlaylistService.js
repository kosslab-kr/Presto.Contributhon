export default {
  playlists: [],

  createPlaylist(name) {
    const newPlaylist = {
      name: name,
      musics: [],
    }

    this.playlists.push(newPlaylist);

    return newPlaylist
  }
}