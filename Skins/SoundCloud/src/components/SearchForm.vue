<template>
  <form class="headerSearch" @submit="filterBySearchText">
    <input 
     v-model="searchText" 
     @change="filterBySearchText"
     type="text" placeholder="Search"
     />
    <button class="headerSearch__submit textHide" type="submit">Search</button>
  </form>
</template>

<script>
export default {
 data(){
   return {
     searchText: '',
   }
 },
 methods: {
   filterBySearchText(e){
     e.preventDefault()
     const searchText = this.searchText
     this.$store.commit('FilterHomeList', searchText)
     this.searchText = '';
   }
 }
}
</script>

<style lang="scss">
.headerSearch {
  flex: 1;
  padding: 9px 10px 8px;
  position: relative;
  color: #999;
  input {
    height: 30px;
    width: 100%;
    font-family: "Interstate", "Lucida Grande", "Lucida Sans Unicode", "Lucida Sans", Garuda, Verdana, Tahoma,
      sans-serif;
    font-weight: 100;
    font-size: 14px;
    line-height: 20px;
    border-radius: 4px;
    color: #666;
    outline: 0;
    border: 0;
    padding: 5px 7px;
    background: #e5e5e5;
  }
  input:focus {
    background: #fff;
  }
}

.headerSearch__submit {
  border: 0;
  padding: 0;
  background: url(../assets/icons/search.svg) center center no-repeat;
  width: 30px;
  height: 30px;
  position: absolute;
  right: 15px;
  top: 50%;
  transform: translateY(-50%);
}
</style>
