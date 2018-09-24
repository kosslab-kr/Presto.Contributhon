<template>
  <div v-if="isActive" class="music-list">
    <div class="field">
      <div class="field__empty"></div>
      <div class="field__title">TITLE</div>
      <div class="field__artist">ARTIST</div>
      <div class="field__album">ALBUM</div>
      <div class="field__time">TIME</div>
      <div class="field__more"></div>
    </div>
    <MusicListItem
      v-for="(music, idx) in musics"
      :key="idx"
      :music="music"
      :index="idx"
      @music-played="playPlayQueue"/>
  </div>
</template>

<script>
import MusicListItem from './MusicListItem.vue';

export default {
  name: 'MusicList',

  components: {
    MusicListItem
  },

  props: {
    name: String,
    musics: Array
  },

  data() {
    return {
      isActive: false
    }
  },

  methods: {
    playPlayQueue(index) {
      this.$emit('playQueue-played', {currentMusicIdx: index, musics: this.musics})
    }
  }
}
</script>

<style scoped lang="scss">
@import '../../index.scss';

.music-list {
  width: 100%;
}

.field {
  width: 100%; line-height: 2rem;
  position: sticky;
  top: 93px;
  background: #191919;
  border-bottom: 1px solid #333;
  overflow: hidden;
  z-index: 1;
}

@mixin field-props {
  color: #999;
  float: left;
  letter-spacing: 1px;
  font-size: 0.8rem;

  &:hover {
    cursor: default;
    color: #fff;
  }
}

.field__empty {
  @include field-props;
  width: 6%; height: 2rem;
  margin-right: 2%;
}

.field__title {
  @include field-props;
  width: 39%;
  margin-right: 5%;
}

.field__artist {
  @include field-props;
  width: 14%;
  margin-right: 5%;
}

.field__album {
  @include field-props;
  width: 14%;
  margin-right: 5%;
}

.field__time {
  @include field-props;
  width: 5%;
  text-align: center;
}

.field__more {
  @include field-props;
  width: 5%; height: 2rem;
}

</style>
