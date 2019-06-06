import request from '@/utils/request'

export function initRoutesData(data) {
  return request({
    url: '/tools/initroutedata',
    method: 'post',
    data
  })
}

export function initEntities(data) {
  return request({
    url: '/tools/initentities',
    method: 'post',
    data
  })
}

export function getEntities() {
  return request({
    url: "/tools/getentities",
    method: "get"
  })
}


export function search(data) {
  return request({
    method: "post",
    url: "/tools/search",
    data
  });
}

export function searchChild(data) {
  return request({
    method: "get",
    url: `/tools/search/${data}`
  });
}

export function start(data) {
  request({
    url: "/tools/startstresstest",
    method: "post",
    data
  })
}

export function abort(id) {
  request({
    url: `/tools/abortstresstest/${id}`,
    method: "get"
  })
}
