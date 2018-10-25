import mockMusicLibrary from "./assets/mockMusicLibrary";
const MusicLibrary = class {
  constructor(audioSelector) {
    this.length = 0;
    this.currentMusic = null;
    this.position = 0;
    this.library = [];
    this.searched = [];
    this.showList = [];

    player.on("positionChanged", () => {
      player.getPosition().then(position => {
        this.position = position;
      });
    });

    player.on("currentMusicChanged", () => {
      player.getCurrentMusic().then(music => {
        this.currentMusic = music;
      });

      player.getLength().then(length => {
        this.length = length;
      });
    });

    library.getMusics().then(musics => {
      this.library = musics;
      this.showList = this.library;
      this.searched = this.showList;
    });
  }
  init() {
    this.showList = this.library;
    this.searched = this.showList;
    // this.setPlayingIdx();
  }
  findShowList(musicId) {
    return this.showList.find(({ id }) => id === musicId);
  }
  mockGetRunningTime() {
    return this.length / 1000;
  }
  getPlayingOne() {
    return this.currentMusic;
  }
  getRunningTime() {
    return this.length;
  }
  setPlayingOne(playingOne) {
    player.playByMusic(playingOne);
  }
  UpdateTime(time) {
    this.position = time;
    player.setPosition(time);

  }
  setPrevIdx() {
    let {
      idx,
      library: { length }
    } = this;
    idx -= 1;
    if (idx < 0) idx = length - 1;
    return (this.idx = idx);
  }
  setNextIdx() {
    let {
      idx,
      library: { length }
    } = this;
    return (this.idx = (idx + 1) % length);
  }
  PlayNextMusic() {
    player.playNext();
  }
  PlayPrevMusic() {
    player.playPrevious();
  }
  setPlayingIdx(activeIdx = 0) {
    this.playingOne = this.library[activeIdx];
    this.mockPlayEl.setAttribute("src", this.playingOne.src);
  }
  play() {
    player.play();
  }
  pause() {

    player.pause();
  }
  next() {
    const nextIdx = this.setNextIdx();
    this.setPlayingIdx(nextIdx);
    this.play();
  }
  getCurrentTime() {
    return this.position;
  }
  getProgressPercent() {
    return (this.getCurrentTime() / this.getRunningTime()) * 100;
  }
  ChangeVolume(percent) {
    player.setVolume(percent / 100);
  }
};

export default MusicLibrary;
