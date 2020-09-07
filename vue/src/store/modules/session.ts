import ajax from '../../lib/ajax';
import util from '../../lib/util'
import store from '../index'
import { VuexModule, Module, Action, Mutation, getModule } from 'vuex-module-decorators'
@Module({ dynamic: true, store, name: 'session' })
class SessionStore extends VuexModule{
    public application = null
    public user = null
    public tenant = null

    @Action
    async init(){
        let rep=await ajax.get('/api/services/app/Session/GetCurrentLoginInformations',{
            headers:{
                'Abp.TenantId': util.abp.multiTenancy.getTenantIdCookie()
            }}
        );
        this.application=rep.data.result.application;
        this.user=rep.data.result.user;
        this.tenant=rep.data.result.tenant;
    }
}
export const moduleSession = getModule(SessionStore)
