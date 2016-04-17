function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}

//Randomizer
function randString(x) {
    var s = "";
    while (s.length < x && x > 0) {
        var r = Math.random();
        s += (r < 0.1 ? Math.floor(r * 100) : String.fromCharCode(Math.floor(r * 26) + (r > 0.5 ? 97 : 65)));
    }
    return s;
}

//Set userIdentifier cookie
if (!(document.cookie.indexOf("l_track_ui=") >= 0))
{
    var dt = new Date();
    dt.setTime(dt.getTime() + 1000 * 60 * 60 * 24 * 30);
    document.cookie = 'l_track_ui=' + randString(64) + ';expires=' + dt.toGMTString() + ';path=/';
}
else
{
    var ui = getCookie('l_track_ui')
    var dt = new Date();
    dt.setTime(dt.getTime() + 1000 * 60 * 60 * 24 * 30);
    document.cookie = 'l_track_ui=' + ui + ';expires=' + dt.toGMTString() + ';path=/';
}

//Set sessionIdentifier cookie
if (!(document.cookie.indexOf("l_track_si=") >= 0))
{
    var dt = new Date();
    dt.setTime(dt.getTime() + 1000 * 60 * 15);
    document.cookie = 'l_track_si=' + randString(64) + ';expires=' + dt.toGMTString() + ';path=/';
}
else
{
    var si = getCookie('l_track_si')
    var dt = new Date();
    dt.setTime(dt.getTime() + 1000 * 60 * 15);
    document.cookie = 'l_track_si=' + si + ';expires=' + dt.toGMTString() + ';path=/';
}

function l_log(d,t, async) {
    var ui = getCookie('l_track_ui');
    var si = getCookie('l_track_si')
    var r = document.referrer;
    var http = new XMLHttpRequest();
    var url = "http://logger.talagozis.com/api/request/track";
    //var url = "http://localhost:50946/request/track";
    var params = "ci=" + ci + "&ui=" + ui + "&si=" + si + "&d=" + d + "&t=" + t + "&r=" + r;

    http.open("POST", url, async);
    http.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    http.send(params);
}

function l_initialize(d) {
    l_log(d, "init", true)
}

function l_track(d)
{
    l_log(d, "track", false)
}

l_initialize("")