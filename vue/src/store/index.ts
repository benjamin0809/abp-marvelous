import Vue from 'vue';
import Vuex from 'vuex';


export interface IRootState {
    app: any,
    account: any,
    role: any,
    session: any,
    tenant: any,
    user: any,
  }
Vue.use(Vuex);
export default new Vuex.Store<IRootState>({})