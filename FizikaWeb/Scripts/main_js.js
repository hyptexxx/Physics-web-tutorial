var navbar = $('.left-menu-container');
var wrapper = $('.main-container');

function contScroll(elem) { //  функция скролла при нажатии на пункт оглавления
    var destination = $(elem).offset().top - 100;

    $('body').animate({
        scrollTop: destination
    }, 1100);
}

function bth() {
    $('body').animate({
        scrollTop: $('body').offset().top - 100
    }, 1000); return false;
}

window.onscroll = function () {  //  функция для отображения кнопки "подняться к шапке сайта" (при скролле)
    var scrolled = window.pageYOffset;
    var elem = document.getElementById("back-to-head");
    if (elem !== null) {
        if (scrolled >= 400) {
            elem.style.display = "block";
        } else {
            elem.style.display = "none";
        }
    } 
}

window.onload = function () {  //   функция запускается при загрузке страницы
    IdController(); // запуск функции которая задает id всем главам текста у которых есть id="t" и class="counter
 //   getSelectionf(); вызов функции отслеживания выделения текста мышью с requestAnimationFrame()
}

function IdController() {  //  функция для задания id (#t1, #t2, #t3, #t4) и т.д.

    var count = document.getElementsByClassName('counter').length; // получаем колличество элементов с классом coumter
    var arr = new Array();  // инициализируем массив для хранения элементов с id = "#t"
    var bl = document.getElementsByClassName("cont-bl");
    var li_count = document.getElementById("li-count");  // получаем контейнер для блоков с оглавлениемa

    if (li_count !== null) {
        while (li_count.firstChild) {
            li_count.removeChild(li_count.firstChild);
        }

        for (var i = 1; i <= count; i++) {  // цикл (пока i <= колличеству элементов с классом counter(колличеству заголовков))
            arr[i] = document.getElementById("t");
            console.log(arr)// помещаем в i элемент массива найденный ЭЛЕМЕНТ с id= "#t"
            arr[i].id = "t" + i;   // задаем задаем i элементу массива id="#t"+i тоесть к примеру
            // 1й элемент массива будет выглядеть так: <h1 id="#t1" class="counter"></h1>
            // 2й элемент массива будет выглядеть так: <h1 id="#t2" class="counter"></h1>
            // 3й элемент массива будет выглядеть так: <h1 id="#t3" class="counter"></h1>
        }
        HtmlIdController();   // вызов функции которая задет атрибуты тэгу <li></li>  (пунктам меню)
    }   
}

function HtmlIdController() { // функция которая задет атрибуты тэгу <li></li>  (пунктам меню)
    var count = document.getElementsByClassName('counter').length;   // получаем колличество элементов с классом coumter
    var li_count = document.getElementById("li-count");  // получаем контейнер для блоков с оглавлением
    var li;
    var t;

    for (var i = 1; i <= count; i++) {  // цикл (пока i <= колличеству элементов с классом counter)
        li = document.createElement('li');  //  создаем новый <li></li>
        t = document.getElementById("t" + i);  // получаем элемент с id = "t"+i к примеру на 1 шаге цикла это будет "#t1"

        li.className = 'cont-bl text-center';  // задаем <li></li> ----> class ----> <li class="cont-bl text-center"></li>
        li.innerHTML = t.innerHTML;  // получаем то что написано в заголовке и помещаем эту надпись в <li></li> (пункт оглавления)
        li.setAttribute('onclick', "contScroll('#t" + i + "')");  // добавляем onclick() с параметром(#t[i])
        console.log("qweqwe");
        li_count.appendChild(li);  // добавляем <li></li>  на страницу помещая его в контейнер
    }
}

function EndOfTest() {
    var id1 = document.getElementById("test_0_1");
    var id2 = document.getElementById("test_1_1");
    var id3 = document.getElementById("test_2_1");
    var id4 = document.getElementById("test_3_1");
    var id5 = document.getElementById("test_4_1");
    var id6 = document.getElementById("test_5_1");
    var id7 = document.getElementById("test_6_1");
    var id8 = document.getElementById("test_7_1");
    var id9 = document.getElementById("test_8_1");

    var counter = 0;

    if (id1.checked == true) { counter++ }
    if (id2.checked == true) { counter++ }
    if (id3.checked == true) { counter++ }
    if (id4.checked == true) { counter++ }
    if (id5.checked == true) { counter++ }
    if (id6.checked == true) { counter++ }
    if (id7.checked == true) { counter++ }
    if (id8.checked == true) { counter++ }
    if (id9.checked == true) { counter++ }

    alert("правильных ответов: " + counter);
    return false;
}
function article_out_controller() {
    if (document.getElementsByClassName("article-out")[0] == null) {
        var parentNode = document.getElementsByClassName("article-container-img")[0];
        var parentNode = document.getElementsByClassName("article-out-img")[0];
        var right_menu_container = document.getElementsByClassName("right-menu-container")[0];
        var elem = document.createElement("div");

        right_menu_container.style.display = "block";
        parentNode.style.background = "#fff";
        parentNode.style.columns = "auto 1";
        while (parentNode.firstChild) {
            parentNode.removeChild(parentNode.firstChild);
        }

        elem.className = "article-out";
        parentNode.appendChild(elem);
    }
} 
function successFunc(data, status) {
    document.getElementsByClassName("article-out")[0].innerHTML = data;
    IdController();
    //   alert("qwe" + data);
}

function errorFunc(errorData) {
    alert('Ошибка' + errorData.responseText);
}

function doAjax(elem) {
    var Book = {
        'name': $(elem).attr("name"),
    }
    $.ajax({
        type: "POST",
        url: "/SectionSelector",
        data: JSON.stringify(Book),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });
}

function DoAjaxSearch(input) {
    var search_input = document.getElementById("search_inp");
    var lengthOfInput = input.value.length;
    if (input == null) {
        input = search_input;
        if (input.value == input.value.replace(/[^\w\s,А-Яа-я()]/, '') && input.value !== "") {
            input.style.background = "white";
            input.style.color = "black";
            var data = input.value;
            console.log(data + "  " + lengthOfInput);
        var uSearch = {
            'param': data
            }
            $.ajax({
                type: "POST",
                url: "/sql_search",
                data: JSON.stringify(uSearch),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: success,
                error: error
            });
        }
        else {
            document.getElementsByClassName("article-out")[0].innerHTML = "введен недопустимый символ";
            input.style.background = "#ffb7b7";
            input.style.color = "white";
            input.value = "";
        }
    }
   else if (input.value == input.value.replace(/[^\w\s,А-Яа-я()]/, '') && input.value !== "") {
        input.style.background = "white";
        input.style.color = "black";
    var data = input.value;
        console.log(data);
    var uSearch = {
            'param': data
        }
        $.ajax({
            type: "POST",
            url: "/sql_search",
            data: JSON.stringify(uSearch),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: success,
            error: error
        });
    }
    else {
       document.getElementsByClassName("article-out")[0].innerHTML = "введен недопустимый символ";
       input.style.background = "#ffb7b7";
       input.style.color = "white";
       input.value = "";
    }
    
}
function success(data) {
    if (data == "false") {
        console.log("ajax is false");
    }
    document.getElementsByClassName("article-out")[0].innerHTML = data;
    IdController();
}

function error(errorData) {
    console.log('Ошибка' + errorData.responseText);
    document.getElementsByClassName("article-out")[0].innerHTML = "Произошла ошибка, попробуйте еще раз.";
}

function doAjaxDeleteRecord() {
    //var response = {
    //    'name': ""
    //}
    //$.ajax({
    //    type: "POST",
    //    url: "/RemoveRecord",
    //    data: JSON.stringify(response),
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: deleteSuccess,
    //    error: deleteError
    //});
}
function deleteSuccess(response) {
    document.location.href = 'http://localhost:4356/';
}
function deleteError(response) {
    document.getElementsByClassName("article-out")[0].innerHTML = "Error";
}
function removeRecord(elem) {
    var name = $(elem).attr("recordName");
    console.log(name);
    var selected_record = {
        'name': name
    }
    $.ajax({
        type: "POST",
        url: "/remove_record",
        data: JSON.stringify(selected_record),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: deleteSuccess,
        error: deleteError
    });
}
function checkForbiddenSymbolls(input) {
    var error_message = document.getElementById("error-message");
    if (input.value == input.value.replace(/[^\w\s,А-Яа-я()]/, '') && input.value !== "") {
        input.style.background = "white";
        input.style.color = "black";
        error_message.innerHTML = "";
    }
    else {
        input.style.background = "#ffb7b7";
        input.style.color = "white";
        input.value = "";
        error_message.innerHTML = "Запрещено вводить символы типа - '~!@#$%^&*{}[]?Ё!№;%:?*";
    }
}
// скрипт для получения выделенного текста 

//function getSelectionf() {
//    requestAnimationFrame(getSelectionf);
//    var target = document.getElementsByClassName("header-container")[0];
//    var selected;

//    if (window.getSelection) {
//        selected = window.getSelection();
//    } else if (document.getSelection) {
//        selected = document.getSelection();
//    } else if (document.selection) {
//        selected = document.selection.createRange().text;
//    }

//    target.innerHTML = "<i>" + selected + "</i>";
//    console.log(selected);
//}