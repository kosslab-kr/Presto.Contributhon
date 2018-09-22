<template>
  <div class="album">
    <div
      class="album__picture-wrap"
      :class="{'album__picture-wrap--pressed': isAlbumPressed}"
      @mousedown="isAlbumPressed = true"
      @mouseup="selectAlbum">
      <img class="album__picture" :src="album.picture" alt="cover1">
      <div class="album__picture-cover">
        <div
          class="album__play-button"
          :class="{'album__play-button--pressed': isPlayButtonPressed}"
          @mousedown.stop="isPlayButtonPressed = true"
          @mouseup.stop="playAlbum">
          <div class="album__play-button-triangle"></div>
        </div>
      </div>
    </div>
    <div class="album__title">{{album.title}}</div>
    <div class="album__artist">{{album.artist}}</div>
  </div>
</template>

<script>
export default {
  name: 'Album',
  
  props: {
    album: Object
  },

  data() {
    return {
      isAlbumPressed: false,
      isPlayButtonPressed: false
    }
  },

  methods: {
    selectAlbum() {
      this.isAlbumPressed = false;
      this.$emit('album-selected', this.album);
    },

    playAlbum() {
      this.isPlayButtonPressed = false;
      this.$emit('album-played', this.album);
    }
  }
}
</script>

<style scoped lang="scss">
@import '../../index.scss';

.album {
  float: left;
  width: 32%;
  margin-right: 2%;
  margin-bottom: 45px;

  &:nth-child(3n) {
    margin-right: 0px;
  }

  @media only screen and (min-width: 950px) {
    width: 23.5%;

    &:nth-child(3n) {
      margin-right: 2%;
    }

    &:nth-child(4n) {
      margin-right: 0px;
    }
  }
}

.album__picture-wrap {
  position: relative;
  width: 100%; height: 0;
  padding-top: 100%;
  margin-bottom: 15px;

  &--pressed {
    transform: scale(0.95);

    // cover
    &::after {
      position: absolute;
      top: 0px; left: 0px;
      content: '';
      display: block;
      width: 100%; height: 0;
      padding-top: 100%;
      background: rgba(0, 0, 0, 0.3);
    }
  }

  &:hover {
    cursor: pointer;

    .album__picture-cover { display: block;}
  }
}

.album__picture-cover {
  display: none;
  position: absolute;
  top: 0px; left: 0px;
  width: 100%; height: 100%;
  background: rgba(0,0,0,0.4);
  text-align: center;

  @include vertical-align-helper;
}

.album__play-button {
  position: relative;
  display: inline-block;
  vertical-align: middle;
  width: 30%; height: 30%;
  background: rgba(0, 0, 0, 0.4);
  border-radius: 50%;
  border: 1px solid #bbb;
  transition: transform 0.05s;
  text-align: center;
  overflow: hidden;
  @include vertical-align-helper;

  &--pressed {
    border-width: 0px;
    transform: scale(1) !important;

    // cover
    &::after {
      @extend .album__picture-wrap--pressed::after;
    }
  }

  &:hover {
    cursor: default;
    border-color: #fff;
    background: rgba(0, 0, 0, 0.7);
    transform: scale(1.1);
  }
}

.album__play-button-triangle {
  display: inline-block;
  vertical-align: middle;
  width: 42%; height: 42%;
  background: no-repeat center/85% url('../../assets/right_arrow.png');
  margin-left: 10%;
}

.album__picture {
  position: absolute;
  top: 0px; left: 0px;
  width: 100%;
}

.album__title {
  line-height: 1.3;
  color: #fff;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.album__artist {
  line-height: 1.3;
  font-size: 0.9rem;
  color: #bbb;
  font-weight: lighter;
}
</style>
