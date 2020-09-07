import Tenant from '../entities/tenant'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import store from '../index'
import { VuexModule, Module, Action, Mutation, getModule } from 'vuex-module-decorators'

@Module({ dynamic: true, store, name: 'tenant' })
class TenantModule extends VuexModule{
    public totalCount = 0
    public currentPage = 1 
    public pageSize = 10
    public list = new Array<Tenant>()
    public loading = false
    public editTenant = new Tenant()

    @Action
    async getAll(payload:any){
        this.loading=true;
        let reponse=await Ajax.get('/api/services/app/Tenant/GetAll',{params:payload.data});
        this.loading=false;
        let page=reponse.data.result as PageResult<Tenant>;
        this.totalCount=page.totalCount;
        this.list=page.items;
    }
    @Action
    async create(payload:any){
        await Ajax.post('/api/services/app/Tenant/Create',payload.data);
    }
    @Action
    async update(payload:any){
        await Ajax.put('/api/services/app/Tenant/Update',payload.data);
    }
    @Action
    async delete(payload:any){
        await Ajax.delete('/api/services/app/Tenant/Delete?Id='+payload.data.id);
    }
    @Action
    async get(payload:any){
        let reponse=await Ajax.get('/api/services/app/Tenant/Get?Id='+payload.id);
        return reponse.data.result as Tenant;
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
    edit(tenant:Tenant){
        this.editTenant=tenant;
    }
}
export const moduleTenant = getModule(TenantModule)
