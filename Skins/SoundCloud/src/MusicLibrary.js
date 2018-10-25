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

    // this.idx = 0;
    // this.volume = 5;
    // this.showList = null;
    // this.searched = null;
    // this.library = mockMusicLibrary;
    // this.playingOne = mockMusicLibrary[0] || null;
    // this.mockPlayEl = document.querySelector(audioSelector);
    // this.init();
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
    // return Math.floor(this.playingOne.runningTime / 1000);
  }
  getPlayingOne() {
    return this.currentMusic;
    // return this.playingOne;
  }
  getRunningTime() {
    //  mock 자동 넘어가는 부분 구현을 위한 mock 처리
    // return this.playingOne.runningTime;
    return this.length;
  }
  setPlayingOne(playingOne) {
    player.playByMusic(playingOne);
    // this.playingOne = playingOne;
    // this.mockPlayEl.setAttribute("src", this.playingOne.src);
  }
  UpdateTime(time) {
    this.position = time;
    player.setPosition(time);
    // this.mockPlayEl.currentTime = time;
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
    // this.setNextIdx();
    // this.setPlayingIdx(this.idx);
    // this.play();
  }
  PlayPrevMusic() {
    // this.setPrevIdx();
    // this.setPlayingIdx(this.idx);
    // this.play();
    player.playPrevious();
  }
  setPlayingIdx(activeIdx = 0) {
    this.playingOne = this.library[activeIdx];
    this.mockPlayEl.setAttribute("src", this.playingOne.src);
  }
  play() {
    // if (this.playingOne === null) return;
    // this.mockPlayEl.play();
    player.play();
  }
  pause() {
    // this.mockPlayEl.pause();
    player.pause();
  }
  next() {
    const nextIdx = this.setNextIdx();
    this.setPlayingIdx(nextIdx);
    this.play();
  }
  getCurrentTime() {
    return this.position;
    // return this.mockPlayEl.currentTime;
  }
  getProgressPercent() {
    return (this.getCurrentTime() / this.getRunningTime()) * 100;
  }
  ChangeVolume(percent) {
    // this.volume = percent;
    player.setVolume(percent / 100);
  }
};

export default MusicLibrary;
