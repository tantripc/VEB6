export type ResponseData<T> = {
    data: T[] | T;
    message: string;
    status: number;
}