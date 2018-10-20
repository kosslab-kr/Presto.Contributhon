<template>
  <BaseContextMenu
    :style="{top: '45px', left: '415px'}"
    :items="moreMenuItems"
    @item-clicked="$emit('menu-closed')"
    @outside-clicked="$emit('menu-closed')"
  />
</template>

<script>
export default {
  name: 'MainMoreMenu',

  data() {
    return {
      moreMenuItems: [
        {
          name: '음악 추가',
          subItems: [
            {
              name: '파일로 음악 추가',
              callback: this.addMusic,
            },
            {
              name: '폴더에서 음악 추가',
              callback: this.addMusicOnFolder,
            },
            {
              name: 'Youtube에서 음악 추가',
              callback: this.addMusicOnYoutube,
            },
          ]
        },
        {
          name: '라이브러리 검색',
          callback: this.searchLibrary,
        },
        {
          name: '프로그램 설정',
          callback: this.configure,
        },
      ],
    }
  },

  methods: {
    searchLibrary() {
      // 라이브러리 검색
    },

    configure() {
      // 프로그램 설정
    },

    addMusic() {
      // 파일로 음악 추가
    },

    addMusicOnFolder() {
      // 폴더에서 음악 추가
    },

    addMusicOnYoutube() {
      // 유튜브에서 음악 추가
    }
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

</style>
