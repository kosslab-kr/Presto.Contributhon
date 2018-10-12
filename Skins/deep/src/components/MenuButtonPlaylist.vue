<template>
  <div
    class="playlist-button"
    @click="toggleButton"
  >
    <div class="playlist-button__icon"></div>
    <div class='playlist-button__text'>New Playlist</div>
    <div
      class="playlist-button__form"
      v-if="isActive"
    >
      <input
        ref="input"
        type="text"
        @keypress="checkEnter"
      >
      <div
        class="playlist-button__submit"
        @click="submitPlaylistName"
      >
        CREATE
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'MenuButtonPlaylist',

  mounted() {
    window.addEventListener('click', ({target}) => {
      if(this.$el.contains(target)) return;

      this.isActive = false;
    })
  },

  data() {
    return {
      isActive: false,
    }
  },

  methods: {
    toggleButton({target}) {
      if(target.closest('.playlist-button__form')) return;
      
      this.isActive = !this.isActive;
      this.$nextTick(() => {
        this.$refs.input.focus();
      })
    },

    submitPlaylistName() {
      const playlistName = this.$refs.input.value;

      if(playlistName === '') {
        console.log('no name playlist!');
        return;
      }

      this.$emit('playlist-name-submitted', playlistName);
      this.isActive = false;
    },

    checkEnter(e) {
      if(e.key !== 'Enter') return;

      this.submitPlaylistName();
    }
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.playlist-button {
  @include position(fixed, $bottom: $player-height);
  @include size($width: $main-menu-width);
  box-sizing: border-box;
  line-height: 55px;
  border-top: 1px solid #333;
  font-weight: lighter;
  font-size: 1rem;
  color: #aaa;
  background: #111;
  padding: 0px 25px;

  &:hover {
    .playlist-button__icon {
      &, &::before, &::after {
        border-color: #fff;
      }
    }
    
    .playlist-button__text {
      color: #fff;
    }
  }
}


.playlist-button__icon {
  @include position(relative);
  @include size($width: 21px, $height: 21px);
  display: inline-block;
  border-radius: 50%;
  $border-style: 1px solid #aaa;
  border: $border-style;
  margin-right: 7px;
  vertical-align: middle;

  @mixin cross($top, $left, $border-side) {
    @include position(absolute, $top: $top, $left: $left);
    @include size($width: 26%, $height: 26%);
    box-sizing: border-box;
    display: block;
    content: '';
    @each $side in $border-side {
      border-#{$side} : $border-style;
    }
  }

  &::before { @include cross($top: 25%, $left: 25%, $border-side: (right, bottom)) }

  &::after { @include cross($top: 48%, $left: 48%, $border-side: (top, left)) }
}

.playlist-button__text {
  display: inline-block;
  vertical-align: middle
}

.playlist-button__form {
  position: absolute;
  top: -120px; left: 0px;
  width: 180px; height: 100px;
  background: red;
}

.playlist-button__submit {
  border: 1px solid black;
}

</style>
