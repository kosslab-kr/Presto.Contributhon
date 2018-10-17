<template>
  <section id='player' class="player">
    <div class="player__wrap">
      <PlayerCurrentMusic
        :music="currentMusic"
      />
      <PlayerControls
        ref="playerControls"
        @music-changed="setCurrentMusic"
      />
      <PlayerVolume/>
    </div>
  </section>
</template>

<script>
import PlayerCurrentMusic from './PlayerCurrentMusic.vue';
import PlayerControls from './PlayerControls.vue';
import PlayerVolume from './PlayerVolume.vue';

export default {
  name: 'Player',

  components: {
    PlayerCurrentMusic,
    PlayerControls,
    PlayerVolume,
  },

  data() {
    return {
      currentMusic: {
        title: 'untitled',
        artist: 'anonymous',
        album: {picture: require('../assets/album_picture/untitled.png')},
      },
    }
  },

  methods: {
    play(music) {
      this.$refs.playerControls.play(music);
    },

    // 다음 음악 세팅
    setCurrentMusic(music) {
      this.currentMusic = music;
    }
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.player {
  box-sizing: border-box;
  position: fixed;
  width: 100vw; height: $player-height;
  bottom: 0px;
  background: #272727;
}

/* for child element(position absolute) */
.player__wrap {
  position: relative;
  width: 100%; height: 100%;
}

</style>
