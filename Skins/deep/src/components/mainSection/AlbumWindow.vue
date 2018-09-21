<template>
  <div class="album-window-wrap">
    <div class="album-window">
      <header class="album-window__header">
        <div
        class="album-window__picture"
        :style="{ background: 'no-repeat center/100% url(' + album.picture + ')' }">
        </div>
        <div class="album-window__description">
          <div class="album-window__title">{{album.title}}</div>
          <div class="album-window__artist"><span class="album-window__artist-by">By</span>{{album.artist}}</div>
        </div>
        <div class="album-window__play-button">PLAY</div>
        <div class="album-window__field">
          <div class="album-window__field-number">#</div>
          <div class="album-window__field-title">TITLE</div>
          <div class="album-window__field-time">TIME</div>
        </div>
      </header>
      <ul class="album-window__music-list">
        <li v-for="(n, idx) in 20" :key="idx" class="album-window__music">
          <div class="album-window__music-number">{{n}}</div>
          <div class="album-window__music-title">This is title</div>
          <div class="album-window__music-time">3:33</div>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
export default {
  name: 'AlbumWindow',

  props: {
    album: Object
  },

  // close AlbumWindow
  mounted() {
    const closeWindow = ({target}) => {
      if(target.closest('.album-window')) return;

      this.$emit('click-outside');
    }

    document.body.addEventListener('click', closeWindow);
  }
}
</script>

<style scoped lang="scss">
@import '../../index.scss';

$picture-size: 100px;

.album-window-wrap {
  position: fixed;
  top: 0px; left: $main-menu-width;
  z-index: 1000; // viewHeader(1) < mainHeader(100) < 1000
  width: calc(100vw - #{$main-menu-width}); height: calc(100vh - #{$player-height});
  background: rgba(0, 0, 0, 0.4);
}

.album-window {
  box-sizing: border-box;
  position: absolute;
  top: 50%; left: 50%;
  transform: translate3d(-50%, -50%, 0);
  width: 60%; height: 70%;
  background: #191919;
  padding: 0px 15px;
  overflow: scroll;
}

.album-window__header {
  position: sticky;
  top: 0px;
  width: 100%;
  background: #191919;
  border-bottom: 1px solid #333;
  padding-top: 15px;
  overflow: hidden;
}

.album-window__picture {
  float: left;
  width: $picture-size; height: $picture-size;
}

.album-window__description {
  box-sizing: border-box;
  float: left;
  margin-left: 10px;
  width: 60%; height: $picture-size;
  padding-top: 40px;
}

.album-window__title {
  color: #fff;
  line-height: 1.3;
  font-weight: 900;
  font-size: 2rem;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.album-window__artist-by {
  color: #999;
  margin: 0 4px;
}

.album-window__artist {
  color: #fff;
  font-weight: lighter;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.album-window__play-button {
  position: absolute;
  top: 20px; right: 15px;
  width: 80px;
  line-height: 25px;
  border-radius: 20px;
  background: $signature-color;
  color: #fff;
  font-size: 0.7rem;
  letter-spacing: 2px;
  text-align: center;

  &:hover {
    cursor: default;
    transform: scale(1.05);
    background: #13f74c;
  }
}

.album-window__field {
  float: left;
  width: 100%; height: 20px;
  margin-top: 15px;
}

@mixin field-props {
  color: #999;
  font-size: 0.9rem;
  text-align: center;
}

.album-window__field-number {
  @include field-props;
  float: left;
  width: 40px;
  margin-right: 20px;
}

.album-window__field-title {
  @include field-props;
  float: left;
}

.album-window__field-time {
  @include field-props;
  float: right;
  width: 60px;
}

.album-window__music-list {
  width: 100%;
}

.album-window__music {
  width: 100%;
  line-height: 2.3rem;
  border-bottom: 1px solid #333;
  overflow: hidden;

  &:hover {
    cursor: default;
    background: #333;
  }
}

@mixin music-props {
  font-size: 0.9rem;
  font-weight: lighter;
}

.album-window__music-number {
  @extend .album-window__field-number;
  @include music-props;
  color: #fff;
}

.album-window__music-title {
  @extend .album-window__field-title;
  @include music-props;
  color: #fff;
}

.album-window__music-time {
  @extend .album-window__field-time;
  @include music-props;
}

</style>
