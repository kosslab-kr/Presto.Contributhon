<template>
  <div id="app">
    <Header :logo="logo" />
    <section class="router__section">
      <NavBar />
    </section>
    <router-view />
    <MusicPlayer :title="title" :cover="cover" />
  </div>
</template>

<script>

import Header from './components/Header.vue'
import NavBar from './components/NavBar.vue'
import MusicPlayer from './components/MusicPlayer.vue'
import MusicPlayerProgress from './components/MusicPlayerProgress.vue'
import {musicTimeFormat} from './helper.js'
// static Asset
import  soundLogo  from "./assets/icons/soundLogo.png";


export default {
  name: 'app',
  data(){
    return {
      logo: soundLogo,
      playList: window.musicLibrary,
    }
  },
  computed: {
    title () {
      if (!this.getPlayingOne) return '';
      return this.getPlayingOne.Title;
    },
    cover () {
      if (!this.getPlayingOne) return '';
      return this.getPlayingOne.Album.Picture;
    },
    getPlayingOne(){
      return this.$store.state.musicLibrary.playingOne
    },
    username () {
      // 곧 `params` 확인할 수 있습니다.
      return this.$route.params.username
    }
  },
  methods: {
    loadRunningTime(time){
      return musicTimeFormat(time)
    },
     goBack () {
      window.history.length > 1
        ? this.$router.go(-1)
        : this.$router.push('/')
    },
  },
  components: {
    Header,
    MusicPlayer,
    MusicPlayerProgress,
    NavBar,
  }
}
</script>

<style lang="scss">
@import "./style/reset.scss";
.router__section {
  padding-left: 20px;
  padding-right: 20px;
}

button {
  background: transparent;
  border: 0;
  padding: 0;
}

.textHide {
  text-indent: -3000px;
}

.album__thumnail {
  margin-right: 15px;
  padding-bottom: 20px;
  & img {
    width: 160px;
    height: 160px;
  }
}

.music__item {
  display: flex;
  flex: 1;
}
.music__content {
  display: flex;
  flex-direction: column;
  flex: 1;
}
.special__section {
  display: flex;
}
.special__buttonlist {
  display: flex;
  li {
    button {
      margin-right: 10px;
    }
  }
}

.soundContext {
  display: flex;
  align-items: center;
  margin-bottom: 12px;
}

.soundContext__box {
  span {
    margin-right: 5px;
    font-size: 12px;
    line-height: 1.4;
    font-family: "Lucida Grande", "Lucida Sans Unicode", "Lucida Sans", Garuda, Verdana, Tahoma, sans-serif;
    color: #333;
  }
}

.genre-avatar {
  width: 30px;
  height: 30px;
  background: url("./assets/images/edmExampleBadge.jpg");
  text-align: center;
  position: relative;
  background-size: cover;
  background-position: 50% 50%;
  background-repeat: no-repeat;
  margin-right: 8px;
  border-radius: 50%;
}
.music__content-header {
  padding-top: 6px;
  display: flex;
  justify-content: flex-start;
  margin-bottom: 10px;
}

.playButton {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 36px;
  height: 36px;
  background: #f50 linear-gradient(#f70, #f30);
  border-radius: 50%;
  margin-right: 8px;
  img: {
    width: 18px;
    height: 24px;
  }
}

.wave__progress {
  .timer {
    width: 100%;
    display: flex;
    justify-content: space-between;
  }
}

.mainmenu__tab {
  padding-top: 30px;
  display: flex;
  border-bottom: 1px solid #f2f2f2;
  li {
    margin-right: 12px;
    font-size: 24px;
    line-height: 1.6;
    &:hover {
      a {
        color: #f30;
        border-bottom: 1px solid #f30;
      }
    }
  }
  a {
    cursor: pointer;
    color: #333;
    border-bottom: 1px solid transparent;
    display: block;
  }
}

.list__description {
  margin-top: 30px;
  margin-bottom: 40px;
  font-size: 18px;
  font-weight: 100;
  color: #999;
}

.wrapper__main {
  padding-left: 20px;
  padding-right: 20px;
}
</style>
