<template>
  <div class="artist">
    <div
      class="artist__picture-wrap"
      :class="{'artist__picture-wrap--pressed': isArtistPressed}"
      @mousedown="isArtistPressed = true"
      @mouseup="selectArtist">
      <img class="artist__picture" :src="artist.picture" alt="artist1">
      <div class="artist__picture-cover">
        <div class="artist__play-button">
          <PlayPauseButton
            :background="'rgba(0,0,0,0.5)'"
            :hoverBackground="'rgba(0,0,0,0.7)'"
            @button-clicked="playArtist"/>
        </div>
      </div>
    </div>
    <div class="artist__name">{{artist.name}}</div>
    <div class="artist__songs">{{artist.musics.length}} SONGS</div>
  </div>
</template>

<script>
import PlayPauseButton from "../PlayPauseButton.vue";

export default {
  name: 'Artist',

  components: {
    PlayPauseButton
  },
  
  props: {
    artist: Object
  },

  data() {
    return {
      isArtistPressed: false,
    }
  },

  methods: {
    selectArtist() {
      this.isArtistPressed = false;
      this.$emit('artist-selected', this.artist);
    },

    playArtist() {
      this.$emit('artist-played', {currentMusicIdx: 0, musics: this.artist.musics});
    }
  }
}
</script>

<style scoped lang="scss">
@import '../../index.scss';

.artist {
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

.artist__picture-wrap {
  position: relative;
  width: 100%; height: 0;
  padding-top: 100%;
  margin-bottom: 15px;
  overflow: hidden;
  border-radius: 50%;

  &--pressed {
    transform: scale(0.95);

    // pressed-cover
    &::after {
      position: absolute;
      top: 0px; left: 0px;
      content: '';
      display: block;
      width: 100%; height: 0;
      padding-top: 100%;
      background: rgba(0, 0, 0, 0.4);
    }
  }

  &:hover {
    cursor: pointer;

    .artist__picture-cover { display: block;}
  }
}

.artist__picture-cover {
  display: none;
  position: absolute;
  top: 0px; left: 0px;
  width: 100%; height: 100%;
  background: rgba(0,0,0,0.55);
  text-align: center;

  @include vertical-align-helper;
}

.artist__play-button {
  position: relative;
  display: inline-block;
  vertical-align: middle;
  width: 32%; height: 32%;
}

.artist__picture {
  position: absolute;
  top: 0px; left: 0px;
  width: 100%;
}

.artist__name {
  line-height: 1.3;
  text-align: center;
  color: #fff;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.artist__songs {
  line-height: 1.3;
  text-align: center;
  font-size: 0.8rem;
  color: #bbb;
  margin-top: 5px;
  letter-spacing: 1px;
  font-weight: lighter;
}
</style>
