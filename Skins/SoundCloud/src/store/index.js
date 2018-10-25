import Vuex from "vuex";
import Vue from "vue";

Vue.use(Vuex);

const store = new Vuex.Store({
  state: {
    musicLibrary: {}
  },
  mutations: {
    FilterHomeList(state, searchText) {
      const lowerSearchText = searchText.toLowerCase();
      state.musicLibrary.searched = state.musicLibrary.searched.filter(({ title }) => {
        return title.toLowerCase().includes(lowerSearchText);
      });
    },
    BackToBeforeSearchList(state) {
      state.musicLibrary.searched = state.musicLibrary.showList;
    },
    Play(state) {
      state.musicLibrary.play();
    },
    UpdateTime(state, time) {
      state.musicLibrary.UpdateTime(time);
    },
    PlayNextMusic(state) {
      state.musicLibrary.PlayNextMusic();
    },
    PlayPrevMusic(state) {
      state.musicLibrary.PlayPrevMusic();
    },
    PlayNewMusic(state, id) {
      const newMusicItem = state.musicLibrary.findShowList(id);
      state.musicLibrary.setPlayingOne(newMusicItem);
      state.musicLibrary.play();
    },
    PauseMusic(state) {
      state.musicLibrary.pause();
    },
    ChangeVolume(state, percent) {
      state.musicLibrary.ChangeVolume(percent);
    }
  }
});

export default store;
