<template>
   <ul class="mainmenu__tab" @click="handlePageRefresh">
       <li>
        <router-link to="/" name="router">
          All
        </router-link>
       </li>
       <li>
        <router-link to="/artists" name="router">
          Artists
        </router-link>
       </li>
      <li>
       <router-link to="/album" name="router">
        Album
       </router-link>
      </li>
      <li>
       <router-link to="/genres" name="router">
        Genres
       </router-link>
      </li>
    </ul>
</template>

<script>
export default {
  methods: {
    handlePageRefresh({target: {name}}){
      if(name!=="router") return;
       this.$store.commit('BackToBeforeSearchList') 
    }
  },
}
</script>

<style lang="scss" scoped>
.mainmenu__tab {
  padding-top: 30px;
  display: flex;
  border-bottom: 1px solid #f2f2f2;
  li {
    margin-right: 12px;
    font-size: 24px;
    line-height: 1.6;
    &:hover {
      a {
        color: #f30;
        border-bottom: 1px solid #f30;
      }
    }
  }
  a {
    cursor: pointer;
    color: #333;
    border-bottom: 1px solid transparent;
    display: block;
  }
}
</style>
