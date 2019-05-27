$(function () {

   
    InitCalendor = function (dragObj,dropObj,DropEvent) {

        $(dragObj).draggable({
            helper: "clone",
            cursor: "move",
        });

        $(dropObj).droppable({
            hoverClass: "DropCellHover",
            drop: DropEvent
        });
    },


    

    DelRow = function (obj) {
        $(obj).closest(".CellRow").remove();
    }

    InitCalendor();

});