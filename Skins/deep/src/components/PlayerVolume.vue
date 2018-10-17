<template>
  <div class="volume">
    <PlayerButtonMute
      ref="muteButton"
      :volume="volume"
      @button-clicked="mute"
    />
    <div class="volume__slider">
      <PlayerSliderHorizon
        ref="volumeSliderHorizon"
        @slider-thumb-down='sliderDown'
        @slider-thumb-move='setVolumeByPercentage'
      />
    </div>
  </div>
</template>

<script>
import PlayerButtonMute from './PlayerButtonMute.vue';
import PlayerSliderHorizon from './PlayerSliderHorizon.vue';
import IPlayerService from './IPlayerService.js';

export default {
  name: 'PlayerVolume',

  components: {
    PlayerButtonMute,
    PlayerSliderHorizon,
  },

  mounted() {
    this.$refs.volumeSliderHorizon.widthPercentage = this.volume * 100;
  },

  data() {
    return {
      volume: 0.5,
    }
  },

  methods: {
    setVolume(volume) {
      IPlayerService.setVolume(volume);
    },
    
    mute(isMuted) {
      const volume = isMuted ? 0 : this.volume;

      this.$refs.volumeSliderHorizon.widthPercentage = volume * 100;
      this.setVolume(volume);
    },

    sliderDown(widthPercentage) {
      this.$refs.muteButton.isMuted = false;
      this.setVolumeByPercentage(widthPercentage);
    },

    setVolumeByPercentage(widthPercentage) {
      this.volume = widthPercentage/100;
      this.setVolume(this.volume);
    },
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.volume {
  position: absolute;
  right: 0px;
  width: 150px; height: 100%;
  @include vertical-align-helper;
}

.volume__slider {
  display: inline-block;
  vertical-align: middle;
  width: 90px; height: 22px;
  margin-left: 2px;
}

</style>
