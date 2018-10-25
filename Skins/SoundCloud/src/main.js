import Vue from "vue";
import App from "./App.vue";
import MusicLibrary from "./MusicLibrary";
import router from "./router";
import store from "./store";

//미들웨어
Vue.prototype.$EventBus = new Vue();
Vue.config.productionTip = false;

Presto.on("load", () => {
  const musicLibrary = new MusicLibrary("#mockAudio");
  window.musicLibrary = musicLibrary;
  store.state.musicLibrary = musicLibrary;

  console.log("Presto Loaded");
  new Vue({
    el: "#app",
    render: h => h(App),
    router,
    store
  });
});
