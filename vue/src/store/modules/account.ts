import ajax from '../../lib/ajax';
import store from '../index'
import { VuexModule, Module, Action, Mutation, getModule } from 'vuex-module-decorators'
@Module({ dynamic: true, store, name: 'account' })
class AccountModule extends VuexModule{
     
    @Action
    async isTenantAvailable(payload:any){
        let rep=await ajax.post('/api/services/app/Account/IsTenantAvailable',payload.data);
        return rep.data.result;
    }
}
export const moduleAccount = getModule(AccountModule)
