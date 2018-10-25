<template>
  <div
    class="playlist-button"
    :class="{'playlist-button--pressed': isButtonPressed}"
    @mousedown="isButtonPressed = true"
    @mouseleave="isButtonPressed = false"
    @click="toggleButton"
  >
    <div class="playlist-button__icon"></div>
    <div class='playlist-button__text'>New Playlist</div>
    <div
      class="playlist-button__form"
      v-if="isActive"
    >
      <div class="playlist-button__form-text">Playlist Name</div>
      <div class="playlist-button__input-wrap">
        <input
          class="playlist-button__input"
          ref="input"
          type="text"
          @keypress="checkEnter"
        >
      </div>
      <div
        class="playlist-button__submit"
        :class="{'playlist-button__submit--pressed': isSubmitPressed}"
        @mousedown="isSubmitPressed = true"
        @mouseleave="isSubmitPressed = false"
        @click="submitPlaylistName"
      >
        CREATE
      </div>
      <div class="playlist-button__form-tail"></div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'MenuButtonPlaylist',

  created() {
    window.addEventListener('click', this.closeCreateForm, true);
    window.addEventListener('contextmenu', this.closeCreateForm, true);
  },

  data() {
    return {
      isButtonPressed: false,
      isActive: false,
      isSubmitPressed: false,
    }
  },

  methods: {
    closeCreateForm({target}) {
      if(this.$el.contains(target)) return;

      this.isActive = false;
    },

    toggleButton({target}) {
      if(target.closest('.playlist-button__form')) return;
      
      this.isButtonPressed = false;
      this.isActive = !this.isActive;

      this.$nextTick(() => {
        if(!this.$refs.input) return;

        this.$refs.input.focus();
      })
    },

    submitPlaylistName() {
      this.isSubmitPressed = false;

      const playlistName = this.$refs.input.value;

      if(playlistName === '') return;

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

.playlist-button--pressed {
  .playlist-button__icon, .playlist-button__text {opacity: 0.7;}
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
  box-sizing: border-box;
  position: absolute;
  top: -160px; left: 20px;
  width: 160px; height: 135px;
  padding: 20px 15px;
  border-radius: 8px;
  background: #272727;
  box-shadow: 0px 0px 20px 2px #111;
}

.playlist-button__form-text {
  color: #bbb;
  width: 100%; line-height: 1.3rem;
  font-weight: normal;
  font-size: 0.9rem;
  margin-bottom: 5px;
}

.playlist-button__input-wrap {
  position: relative;
  box-sizing: border-box;
  width: 100%;
  border-radius: 4px;
  padding: 0 5px;
  line-height: 1.6rem;
  background: #fff;
  margin-bottom: 20px;
}

.playlist-button__input {
  width: 100%;
  border: none;
  outline: none;
  font-size: 0.9rem;
}

.playlist-button__submit {
  width: 70%; line-height: 25px;
  border-radius: 20px;
  background: $signature-color;
  color: #fff;
  font-size: 0.7rem;
  letter-spacing: 2px;
  font-weight: normal;
  text-align: center;
  margin: auto;

  &:hover {
    transform: scale(1.05);
    background: #13f74c;
  }

  &--pressed {
    transform: scale(1) !important;
    opacity: 0.7;
  }
}

.playlist-button__form-tail {
  position: absolute;
  left: calc(50% - 10px); bottom: -13px;
  width: 0px; height: 0px;
  border-top: 13px solid #272727;
  border-left: 10px solid transparent;
  border-right: 10px solid transparent;
}

</style>
