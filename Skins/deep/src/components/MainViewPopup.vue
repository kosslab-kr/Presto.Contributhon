<template>
  <div
    class="popup"
    :class="{'popup--artist': type === 'artist'}"
    @mouseover="isMouseOvered = true"
    @mouseleave="isMouseOvered = false"
  >
    <header class="popup__header">
      <div
        class="popup__picture-wrap"
      >
        <img
          class="popup__picture"
          :src="picture"
          alt="group_image"
        >
      </div>
      <div class="popup__description-wrap">
        <div class="popup__description">
          <div class="popup__title">
            <BaseTextRolling
              :onPlay="isMouseOvered"
              :text="group.Name"
              :fontStyle="{'color': '#fff', 'font-size': '2rem', 'font-weight': '700'}"
              :delay="1"
            />
          </div>
          <div
            v-if="type === 'album'"
            class="popup__artist"
          >
            <span class="popup__artist-by">By</span>
            {{group.Artist.Name}}
          </div>
        </div>
      </div>
      <div
        class="popup__play-button"
        :class="{'popup__play-button--pressed': isPlayButtonPressed}"
        @mousedown="isPlayButtonPressed = true"
        @mouseup="playGroup"
      >PLAY
      </div>
    </header>
    <div class="popup__body">
      <slot name="body"></slot>
    </div>
  </div>
</template>

<script>
export default {
  name: 'MainViewPopup',

  props: {
    type: {
      validator(value) {
        return (value === 'album') || (value === 'artist');
      }
    },
    group: Object
  },

  data() {
    return {
      isPlayButtonPressed: false,
      isMouseOvered: false,
      picture: '',
    }
  },

  // close Popup
  created() {
    const closePopup = function({target}) {
      if(target.closest('.popup')) return;

      this.$emit('popup-closed');
    }

    document.body.addEventListener('click', closePopup.bind(this));

    // initialize picture
    this.group.getMusics().then(musics => {
      this.picture = this.type === 'artist' ? musics[0].Album.Picture : this.group.Picture;
    });
  },

  methods: {
    async playGroup() {
      const musics = await this.group.getMusics();

      this.isPlayButtonPressed = false;
      this.$emit('group-played', musics[0]);
    },
  },
}
</script>

<style scoped lang="scss">
@import '../index.scss';

$picture-size: 100px;
$popup-width: 50vw;
$popup-height: 70vh;

.popup {
  box-sizing: border-box;
  width: $popup-width; height: $popup-height;
  background: #191919;
  border-radius: 10px;
  padding: 15px;
  box-shadow: 2px 2px 20px 5px #070707;
  z-index: 1000; // MainView__header(1) < Main__header(100) = MainView_popupwrap(100)
  text-align: left;
}

.popup--artist {
  .popup__picture-wrap { border-radius: 50%; }
}

.popup__header {
  position: relative;
  width: 100%;
  background: #191919;
  overflow: hidden;
  margin-bottom: 15px;
}

.popup__picture-wrap {
  float: left;
  width: $picture-size; height: $picture-size;
}

.popup__picture {
  width: 100%; height: 100%;
}

.popup__description-wrap {
  position: relative;
  box-sizing: border-box;
  float: left;
  margin-left: 14px;
  width: 55%; height: $picture-size;
}

.popup__description {
  position: absolute;
  left: 0px; bottom: 5px;
  width: 100%;
}

.popup__title {
  line-height: 1.3;
}

.popup__artist-by {
  color: #999;
  margin: 0 4px;
}

.popup__artist {
  color: #fff;
  font-weight: lighter;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.popup__play-button {
  position: absolute;
  top: 10px; right: 10px;
  width: 80px;
  line-height: 25px;
  border-radius: 20px;
  background: $signature-color;
  color: #fff;
  font-size: 0.7rem;
  letter-spacing: 2px;
  text-align: center;
  overflow: hidden;

  &--pressed {
    transform: scale(1) !important;

    // cover
    &::after {
      position: absolute;
      top: 0px; left: 0px;
      content: '';
      width: 100%; height: 100%;
      background: rgba(0, 0, 0, 0.4);
    }
  }

  &:hover {
    cursor: default;
    transform: scale(1.05);
    background: #13f74c;
  }
}

.popup__body {
  height: calc(#{$popup-height} - #{$picture-size} - 45px);
}

</style>
