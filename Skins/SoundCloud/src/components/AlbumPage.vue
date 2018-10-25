<template>
  <div id="album">
    <section class="wrapper__main">
      <h1 class="list__description">More of what you like</h1>
      <p>Suggestions based on what you've liked or played</p>
      <section class="main__ablumSlider">매인 앨범</section>
      <section class="hotAlbumList">hotAlbum</section>
      <section class="artistSlider">아티스트</section>
      <section class="chartsSlider">챠트 슬라이더</section>
    </section>
  </div>
</template>

<script>

import Header from './Header.vue'
import MusicPlayer from './MusicPlayer.vue'
import MusicPlayerProgress from './MusicPlayerProgress.vue'
import {musicTimeFormat} from '../helper.js'

export default {
  name: 'album',
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
