$(function () {

    var UserApplyCourseKey = "UserApplyCourse";

    var UserBackendKey = "UserBackend";


    //UserBackend -Begin
    SetLocal_UserBackend = function (jsonObj) {

        var json = ""
        if (jsonObj != null) {
            json = JSON.stringify(jsonObj);
            localStorage.setItem(UserBackendKey, json);
        }
            
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



    SetSessonUserApplyCourse = function (jsonObj) {
        var json = ""
        if (jsonObj != null) {
            json = JSON.stringify(jsonObj);
            sessionStorage.setItem(UserApplyCourseKey, json);
        }
           
    }

    GetSessonUserApplyCourse = function () {
        var json = sessionStorage.getItem(UserApplyCourseKey);
        if (json)
            return JSON.parse(json);
        else
            return null;
    }
});