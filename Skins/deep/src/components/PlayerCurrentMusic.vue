<template>
  <div class="music">
    <img
      class="music__album-picture"
      :src="music.Album.Picture"
      alt="music__album-picture">
    <div class="music__title">{{music.Title}}</div>
    <div class="music__artist">{{music.Artist.Name}}</div>
  </div>
</template>

<script>
export default {
  name: 'PlayerCurrentMusic',
  
  props: {
    music: Object
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.music {
  position: absolute;
  left: 0px;
  width: 270px; height: 100%;
}

.music__album-picture {
  display: block;
  float: left;
  $album-cover-size: 50px !global;
  width: $album-cover-size; height: $album-cover-size;
  background: #fff;
  margin: ($player-height - $album-cover-size)/2;
}

.music__title {
  box-sizing: border-box;
  float: left;
  width: 190px; height: $album-cover-size/2;
  color: #fff;
  margin-top: ($player-height - $album-cover-size)/2;
  padding-top: 10px;
  vertical-align: middle;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  @include vertical-align-helper;
}

.music__artist {
  box-sizing: border-box;
  float: left;
  width: 190px; height: $album-cover-size/2;
  color: #ccc;
  font-size: 0.8rem;
  vertical-align: middle;
  @include vertical-align-helper;
}

</style>
