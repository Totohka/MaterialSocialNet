export const objectToFormData = (
  obj: Record<string, string | File | Blob | number | Date>
) => {
  const formData = new FormData();
  for (const key in obj) {
    let value = typeof obj[key] === 'number' ? obj[key].toString() : obj[key];
    value = value instanceof Date ? value.toISOString() : value;
    formData.append(key, value);
  }
  return formData;
};
