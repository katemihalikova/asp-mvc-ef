let $form = $("form");
let destinationId = $("form").data("destinationId");
let $inputs = $("input.form-control");
let $attendees = $("#Attendees");
let $timeslotId = $("#TimeslotID");
let $timeslotsArea = $("#timeslotsArea");
let $submitButton = $("input[type=submit]");

$form.on("submit", e => {
    e.preventDefault();
    if ($timeslotId.val()) {
        window.location.href = `/Orders/Create?timeslotId=${$timeslotId.val()}&attendees=${$attendees.val()}`
    }
});

$inputs.on("change", () => {
    $timeslotId.val(undefined);
    $timeslotsArea.empty();
    $submitButton.prop("disabled", true);
    if ($form.validate().checkForm()) {
        $timeslotsArea.load(`/Destinations/TimeslotsPartial/${destinationId}?${$inputs.serialize()}`);
    }
});

$timeslotsArea.on("click", "button", function() {
    let $this = $(this);
    $timeslotsArea.find("button").addClass("btn-light").removeClass("btn-success");
    $this.addClass("btn-success").removeClass("btn-light");
    $timeslotId.val($this.data("timeslot-id"));
    $submitButton.prop("disabled", false);
});
