import { appRouters, otherRouters } from '../../router/router'
import Util from '../../lib/util';
import store from '../index'
import { VuexModule, Module, Action, Mutation, getModule } from 'vuex-module-decorators'
import ajax from '../../lib/ajax'
import appconst from '../../lib/appconst'
@Module({ dynamic: true, store, name: 'app' })
class AppModule extends VuexModule {
    public cachePage = []
    public lang = ''
    public isFullScreen = false
    public openedSubmenuArr = []
    public menuTheme = 'dark'
    public themeColor = ''
    public pageOpenedList = [{
        meta: { title: 'HomePage' },
        path: '',
        name: 'home',
        argu: '',
        query: ''
    }]

    public currentPageName = ''
    public currentPath = [
        {
            meta: { title: 'HomePage' },
            path: '',
            name: 'home'
        }
    ]
    public menuList = []
    public routers = [
        otherRouters,
        ...appRouters
    ]
    public tagsList = [...otherRouters.children]
    public messageCount = 0
    public dontCache: []
    public noticeList: [{ read: false, type: 0, title: 'First notice', description: 'One day ago' }, { read: false, type: 1 }, { read: false, type: 0, title: 'Second notice', description: 'One month ago' }]

    @Mutation
    logout() {
        localStorage.clear();
        sessionStorage.clear();
    }
    @Mutation
    setTagsList(list: Array<any>) {
        this.tagsList.push(...list);
    }
    @Mutation
    updateMenulist() {
        let menuList: Array<Router> = [];
        appRouters.forEach((item, index) => {
            if (item.permission !== undefined) {
                let hasPermissionMenuArr: Array<Router> = [];
                hasPermissionMenuArr = item.children.filter(child => {
                    if (child.permission !== undefined) {
                        if (Util.abp.auth.hasPermission(child.permission)) {
                            return child;
                        }
                    } else {
                        return child;
                    }
                });
                if (hasPermissionMenuArr.length > 0) {
                    item.children = hasPermissionMenuArr;
                    menuList.push(item);
                }
            } else {
                if (item.children.length === 1) {
                    menuList.push(item);
                } else {
                    let len = menuList.push(item);
                    let childrenArr = [];
                    childrenArr = item.children.filter(child => {
                        return child;
                    });
                    let handledItem = JSON.parse(JSON.stringify(menuList[len - 1]));
                    handledItem.children = childrenArr;
                    menuList.splice(len - 1, 1, handledItem);
                }
            }
        });
        this.menuList = menuList;
    }
    @Mutation
    changeMenuTheme(theme: string) {
        this.menuTheme = theme;
    }
    @Mutation
    changeMainTheme(mainTheme: string) {
        this.themeColor = mainTheme;
    }
    @Mutation
    addOpenSubmenu(name: any) {
        let hasThisName = false;
        let isEmpty = false;
        if (name.length === 0) {
            isEmpty = true;
        }
        if (this.openedSubmenuArr.indexOf(name) > -1) {
            hasThisName = true;
        }
        if (!hasThisName && !isEmpty) {
            this.openedSubmenuArr.push(name);
        }
    }
    @Mutation
    closePage(name: any) {
        this.cachePage.forEach((item, index) => {
            if (item === name) {
                this.cachePage.splice(index, 1);
            }
        });
    }
    @Mutation
    initCachepage() {
        if (localStorage.cachePage) {
            this.cachePage = JSON.parse(localStorage.cachePage);
        }
    }
    @Mutation
    removeTag(name: string) {
        this.pageOpenedList.map((item, index) => {
            if (item.name === name) {
                this.pageOpenedList.splice(index, 1);
            }
        });
    }
    @Mutation
    pageOpenedList1(get: any) {
        let openedPage = this.pageOpenedList[get.index];
        if (get.argu) {
            openedPage.argu = get.argu;
        }
        if (get.query) {
            openedPage.query = get.query;
        }
        this.pageOpenedList.splice(get.index, 1, openedPage);
        localStorage.pageOpenedList = JSON.stringify(this.pageOpenedList);
    }
    @Mutation
    clearAllTags() {
        this.pageOpenedList.splice(1);
        this.cachePage.length = 0;
        localStorage.pageOpenedList = JSON.stringify(this.pageOpenedList);
    }
    @Mutation
    clearOtherTags(vm: Vue) {
        let currentName = vm.$route.name;
        let currentIndex = 0;
        this.pageOpenedList.forEach((item, index) => {
            if (item.name === currentName) {
                currentIndex = index;
            }
        });
        if (currentIndex === 0) {
            this.pageOpenedList.splice(1);
        } else {
            this.pageOpenedList.splice(currentIndex + 1);
            this.pageOpenedList.splice(1, currentIndex - 1);
        }
        let newCachepage = this.cachePage.filter(item => {
            return item === currentName;
        });
        this.cachePage = newCachepage;
        localStorage.pageOpenedList = JSON.stringify(this.pageOpenedList);
    }
    @Mutation
    setOpenedList() {
        this.pageOpenedList = localStorage.pageOpenedList ? JSON.parse(localStorage.pageOpenedList) : [otherRouters.children[0]];
    }
    @Mutation
    setCurrentPath(pathArr: Array<any>) {
        this.currentPath = pathArr;
    }
    @Mutation
    setCurrentPageName(name: string) {
        this.currentPageName = name;
    }
    @Mutation
    clearOpenedSubmenu() {
        this.openedSubmenuArr.length = 0;
    }
    @Mutation
    increateTag(tagObj: any) {
        if (!Util.oneOf(tagObj.name, this.dontCache)) {
            this.cachePage.push(tagObj.name);
            localStorage.cachePage = JSON.stringify(this.cachePage);
        }
        this.pageOpenedList.push(tagObj);
    }

    @Action
    async login(payload: any) {
        let rep = await ajax.post("/api/TokenAuth/Authenticate", payload.data);
        var tokenExpireDate = payload.data.rememberMe ? (new Date(new Date().getTime() + 1000 * rep.data.result.expireInSeconds)) : undefined;
        Util.abp.auth.setToken(rep.data.result.accessToken, tokenExpireDate);
        Util.abp.utils.setCookieValue(appconst.authorization.encrptedAuthTokenName, rep.data.result.encryptedAccessToken, tokenExpireDate, Util.abp.appPath)
    }
}
export const moduleApp = getModule(AppModule)

