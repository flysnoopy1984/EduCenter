$(function () {

    var UserApplyCourseKey = "UserApplyCourse";

    SetSessonUserApplyCourse = function (jsonObj) {
        var json = ""
        if (jsonObj != null)
            json = JSON.stringify(jsonObj);
        sessionStorage.setItem(UserApplyCourseKey, json);
    }

    GetSessonUserApplyCourse = function () {
        var json = sessionStorage.getItem(UserApplyCourseKey);
        if (json)
            return JSON.parse(json);
        else
            return null;
    }
});