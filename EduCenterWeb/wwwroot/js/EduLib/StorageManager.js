$(function () {

    //var UserApplyCourseKey = "UserApplyCourse";
    var UserBackendKey = "UserBackend";
    var UserBuyTimeKey = "UserBuyTime";


    //UserBackend -Begin
    SetLocal_UserBackend = function (jsonObj) {

        SetStorage(UserBackendKey, jsonObj, false);
      
    }
    GetLocal_UserBackend = function () {
        var json = localStorage.getItem(UserBackendKey);
        if (json)
            return JSON.parse(json);
        else
            return null;
    }
    RemoveLocal_UserBackend = function () {
        localStorage.removeItem(UserBackendKey);
    }
    //UserBackend-End 

    //购买课时 -Begin
    //{ "priceCode": priceCode, "VIPQty": vipQty, "payAmount": PayAmount};
    SetSessionBuyCourseTime = function (jsonObj) {
        SetStorage(UserBuyTimeKey, jsonObj, true);
    }

    GetSessionBuyCourseTime = function () {
        return GetStorage(UserBuyTimeKey, true);
    }

    //购买课时 -End
    GetStorage = function (key, isSession) {
        var json;
        if (isSession)
            json = sessionStorage.getItem(key);
        else
            json = localStorage.getItem(key);

        if (json)
            return JSON.parse(json);
        else
            return null;
    }
    SetStorage = function (key, jsonObj,isSession) {
        if (jsonObj != null) {
            var json = JSON.stringify(jsonObj);
            if (isSession)
                sessionStorage.setItem(key, json);
            else
                localStorage.setItem(key, json);
        }

    }

    
});