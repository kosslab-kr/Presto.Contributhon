import Vue from 'vue';
import App from './App.vue';

Vue.config.productionTip = false;

try {
    Presto.on('load', () => {
        new Vue({
            render: h => h(App)
        }).$mount('#app');
    });
}
catch (e) {
    new Vue({
        render: h => h(App)
    }).$mount('#app');
}


console.log(Presto);