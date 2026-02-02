// Copyright 2018 Trinkler Software AG (CH).
// Trinkler Software provides free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version <http://www.gnu.org/licenses/>.

// Module imports:
// ... Vue et al;
import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
// ... Application entry point;
import App from './App.vue';
// ... bootstrap;
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';

// Extend Vue.
Vue.use(BootstrapVue);

// Instantiate root Vue instance.
const vm = new Vue({
    el: '#app',
    render: (h) => h(App),
});
