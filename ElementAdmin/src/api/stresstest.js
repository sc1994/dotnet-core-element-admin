import request from '@/utils/request'

export function start(data) {
  request({
    url: "/stresstest",
    method: "post",
    data
  })
}
