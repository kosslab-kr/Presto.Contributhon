<template>
  <div class="group" :class="{'group--artist': type === 'artist'}">
    <div
      class="group__picture-wrap"
      :class="{'group__picture-wrap--pressed': isPicturePressed}"
      @mousedown="isPicturePressed = true"
      @mouseleave="isPicturePressed = false"
      @click="selectPicture"
    >
      <img class="group__picture" :src="picture" alt="groupPicture">
      <div class="group__picture-cover">
        <div class="group__play-button">
          <BaseButtonPlayPause
            :background="'rgba(0,0,0,0.5)'"
            :hoverBackground="'rgba(0,0,0,0.7)'"
            @button-clicked="playGroup"
          />
        </div>
      </div>
    </div>
    <div class="group__name">{{group.Name}}</div>
    <div class="group__description">{{description}}</div>
  </div>
</template>

<script>
export default {
  name: 'MainViewGroup',
  
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
      isPicturePressed: false,
    }
  },

  methods: {
    selectPicture() {
      this.isPicturePressed = false;
      this.$emit('picture-selected', this.group);
    },

    async playGroup() {
      const musics = await this.group.getMusics();
      
      this.$emit('group-played', musics[0]);
    }
  },

  computed: {
    description() {
      return this.type === 'artist' ? this.group.getMusics().length + ' SONGS' : this.group.Artist.Name;
    },

    picture() {
      return this.type === 'artist' ? this.group.getMusics()[0].Album.Picture : this.group.Picture;
    },
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.group {
  float: left;
  width: 23.5%;
  margin-right: 2%;
  margin-bottom: 45px;

  &:nth-child(4n) {
    margin-right: 0px;
  }

  @media only screen and (min-width: 1000px) {
    width: 18.4%;

    &:nth-child(4n) {
      margin-right: 2%;
    }

    &:nth-child(5n) {
      margin-right: 0px;
    }
  }

  @media only screen and (min-width: 1200px) {
    width: 15%;

    &:nth-child(5n) {
      margin-right: 2%;
    }

    &:nth-child(6n) {
      margin-right: 0px;
    }
  }
}

.group--artist {
  .group__picture-wrap { border-radius: 50%; }
  .group__name { text-align: center; }
  .group__description {
    text-align: center;
    margin-top: 5px;
  }
}

.group__picture-wrap {
  position: relative;
  width: 100%; height: 0;
  padding-top: 100%;
  margin-bottom: 15px;
  overflow: hidden;

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

    .group__picture-cover { display: block;}
  }
}

.group__picture-cover {
  display: none;
  position: absolute;
  top: 0px; left: 0px;
  width: 100%; height: 100%;
  background: rgba(0,0,0,0.55);
  text-align: center;

  @include vertical-align-helper;
}

.group__play-button {
  position: relative;
  display: inline-block;
  vertical-align: middle;
  width: 38%; height: 38%;
}

.group__picture {
  position: absolute;
  top: 0px; left: 0px;
  width: 100%;
}

.group__name {
  line-height: 1.3;
  color: #fff;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.group__description {
  line-height: 1.3;
  font-size: 0.9rem;
  color: #bbb;
  font-weight: lighter;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
</style>
