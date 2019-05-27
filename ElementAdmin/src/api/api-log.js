import request from '@/utils/request'

export function search(data) {
  return request({
    method: "post",
    url: "/apilog/search",
    data
  });
}

export function searchChild(data) {
  return request({
    method: "get",
    url: `/apilog/${data}`
  });
}
