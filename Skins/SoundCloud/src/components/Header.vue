<template>
  <header class="header">
      <div class="header__inner">
        <a href="" class="logo__link">
        <h1 class="logo__heading">
          <img :src="logo" alt="">
        </h1>
      </a>
      <nav class="main__nav">
        <ul>
          <li><a href="" class="active">Home</a></li>
          <li><a href="">Collection</a></li>
        </ul> 
      </nav>
      <SearchForm
      />
      <div class="uploadBox">
        <a href="">Upload</a>
      </div>
      <nav class="user__nav">
       <ul>
        <li><a href="" class="active">U</a></li>
        <li><a href="">N</a></li>
        <li><a href="">M</a></li>
        </ul> 
      </nav>
      </div>
  </header>
</template>

<script>
import SearchForm from './SearchForm'

export default {
  props: ['logo'],
  components: {
    SearchForm,
  }
}
</script>
<style lang="scss" scoped>
.header {
  width: 100%;
  background: #333;
  height: 50px;
  display: flex;
  align-items: center;
}
.header__inner {
  flex: 1;
  align-items: center;
  padding: 0 30px;
  margin: 0 auto;
  display: flex;
}
.logo__link {
  display: inline-flex;
  width: 70px;
  height: 50px;
  align-items: center;
  justify-content: center;
  background: #f50 linear-gradient(#f70, #f30);
}
.logo__heading {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 50px;
}
.main__nav {
  ul {
    display: flex;
  }
  a {
    display: flex;
    align-items: center;
    justify-content: center;
    padding: 12px 0;
    width: 104px;
    height: 46px;
    border-right: 1px solid #111;
    text-align: center;
    color: #999;
    &:hover {
      color: #fff;
    }
  }
  .active {
    background: #111;
    color: #fff;
  }
}

// upload
.uploadBox {
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 12px;
  height: 46px;
  text-align: center;
  color: #999;
  &:hover {
    color: #fff;
  }
}
// userNav
.user__nav {
  ul {
    display: flex;
    a {
      margin-left: 10px;
      margin-right: 10px;
    }
  }
}
</style>
