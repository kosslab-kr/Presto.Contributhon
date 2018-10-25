const path = require('path');

module.exports = {
  baseUrl: '',
  configureWebpack: {
    resolve: {
      alias: {
        '@': path.resolve('src')
      }
    }
  }
}