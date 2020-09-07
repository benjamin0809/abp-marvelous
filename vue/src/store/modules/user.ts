
import User from '../entities/user'
import Role from '../entities/role'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';

import store from '../index'
import { VuexModule, Module, Action, Mutation, getModule } from 'vuex-module-decorators'

@Module({ dynamic: true, store, name: 'user' })
class UserModule extends VuexModule{
    public totalCount = 0
    public currentPage = 1 
    public pageSize = 10
    public list = new Array<User>()
    public loading = false
    public editUser = new User()
    public roles = new Array<Role>()

    @Action
    async getAll(payload:any){
        this.loading=true;
        let reponse=await Ajax.get('/api/services/app/User/GetAll',{params:payload.data});
        this.loading=false;
        let page=reponse.data.result as PageResult<User>;
        this.totalCount=page.totalCount;
        this.list=page.items;
    }

    @Action
    async create(payload:any){
        await Ajax.post('/api/services/app/User/Create',payload.data);
    }

    @Action
    async update(payload:any){
        await Ajax.put('/api/services/app/User/Update',payload.data);
    }
    @Action
    async delete(payload:any){
        await Ajax.delete('/api/services/app/User/Delete?Id='+payload.data.id);
    }
    @Action
    async get(payload:any){
        let reponse=await Ajax.get('/api/services/app/User/Get?Id='+payload.id);
        return reponse.data.result as User;
    }
    @Action
    async getRoles(){
        let reponse=await Ajax.get('/api/services/app/User/GetRoles');
        this.roles=reponse.data.result.items as Role[];
    }
    @Action
    async changeLanguage(payload:any){
        await Ajax.post('/api/services/app/User/ChangeLanguage',payload.data);
    } 
    @Mutation
    setCurrentPage(page:number){
        this.currentPage=page;
    }
    @Mutation
    setPageSize(pagesize:number){
        this.pageSize=pagesize;
    }
    @Mutation
    edit(user:User){
        this.editUser=user;
    }
}
export const moduleUser = getModule(UserModule)