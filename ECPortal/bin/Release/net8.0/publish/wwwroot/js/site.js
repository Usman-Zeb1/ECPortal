$(document).ready(function () {
    var today = new Date().toISOString().split('T')[0];
    $('input[type="date"]').each(function () {
        if (!$(this).val()) {
            $(this).val(today);
        }
    });

 

    function getFormattedDate(dateString) {
        const date = new Date(dateString);
        const day = date.getDate();
        const suffix = getDaySuffix(day);
        const month = date.toLocaleString('default', { month: 'short' });
        const year = date.getFullYear();
        return `${day}${suffix} ${month}, ${year}`;
    }

    function getDaySuffix(day) {
        if (day > 3 && day < 21) return 'th'; // Covers 4th-20th
        switch (day % 10) {
            case 1: return 'st';
            case 2: return 'nd';
            case 3: return 'rd';
            default: return 'th';
        }
    }

});



