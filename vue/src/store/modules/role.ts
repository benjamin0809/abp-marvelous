import Role from '../entities/role'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import store from '../index'
import { VuexModule, Module, Action, Mutation, getModule } from 'vuex-module-decorators'
@Module({ dynamic: true, store, name: 'role' })
class RoleModule extends VuexModule{
    public totalCount = 0
    public currentPage = 1 
    public pageSize = 10
    public list = new Array<Role>()
    public loading = false
    public editRole = new Role()
    public permissions = new Array<string>()

    @Action
    async getAll(payload:any){
        this.loading=true;
        let reponse=await Ajax.get('/api/services/app/Role/GetAll',{params:payload.data});
        this.loading=false;
        let page=reponse.data.result as PageResult<Role>;
        this.totalCount=page.totalCount;
        this.list=page.items;
    }
    @Action
    async create(payload:any){
        await Ajax.post('/api/services/app/Role/Create',payload.data);
    }
    @Action
    async update(payload:any){
        await Ajax.put('/api/services/app/Role/Update',payload.data);
    }
    @Action
    async delete(payload:any){
        await Ajax.delete('/api/services/app/Role/Delete?Id='+payload.data.id);
    }
    @Action
    async get(payload:any){
        let reponse=await Ajax.get('/api/services/app/Role/Get?Id='+payload.id);
        return reponse.data.result as Role;
    }
    @Action
    async getAllPermissions(){
        let reponse=await Ajax.get('/api/services/app/Role/getAllPermissions');
        this.permissions=reponse.data.result.items;
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
    edit(role:Role){
        this.editRole=role;
    }
}
export const moduleRole = getModule(RoleModule)