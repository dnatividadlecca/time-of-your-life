const numberToWords = (num) => {
    const words = [
        "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten",
        "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"
    ];
    const tens = ["", "", "twenty", "thirty", "forty", "fifty"];

    if (num < 20) {
        return words[num];
    }
    const [tensPart, unitsPart] = [Math.floor(num / 10), num % 10];
    return tensPart < 2 ? words[num] : tens[tensPart] + (unitsPart ? `-${words[unitsPart]}` : "");
};

const convertTimeToText = (timeString) => {
    let [hour, minute, second] = timeString.split(":").map(Number);
    const period = hour >= 12 ? "PM" : "AM";
    hour = hour % 12 || 12;

    const hourText = `${numberToWords(hour)}`;
    const minuteText = minute < 10 ? `oh ${numberToWords(minute)}` : `${numberToWords(minute)}`;
    const secontText = `and ${numberToWords(second)} seconds`;

    return `${hourText} ${minuteText} ${secontText} ${period}`;
};

export default convertTimeToText;