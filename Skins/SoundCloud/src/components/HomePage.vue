<template>
  <div id="home">
    <section class="wrapper__main">
      <p class="list__description">Hear the latest posts from the people you’re following:</p>
      <MusicList :musicList="musicList"/>
    </section>
  </div>
</template>

<script>

import MusicPlayerProgress from './MusicPlayerProgress.vue'
import MusicList from './MusicList'
import {musicTimeFormat} from '../helper.js'


export default {
  name: 'home',
   data(){
    return {
      isPlayButton: true,
    }
  },
  computed: {
    musicList(){
      return this.$store.state.musicLibrary.searched;
    },
  },
  components: {
    MusicPlayerProgress,
    MusicList,
  }
}
</script>

<style lang="scss">
@import "../style/reset.scss";
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
  background: url("../assets/images/edmExampleBadge.jpg");
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
</style>
